using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using CottonPrompt.Infrastructure.Constants;
using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.Designs;
using CottonPrompt.Infrastructure.Models.Orders;
using Microsoft.EntityFrameworkCore;
using System.Net.Mail;
using System.Net;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public class OrderService(CottonPromptContext dbContext, BlobServiceClient blobServiceClient, IConfiguration config) : IOrderService
    {
        public async Task ApproveAsync(int id)
        {
            try
            {
                var order = await dbContext.Orders.Include(o => o.OrderDesigns).SingleOrDefaultAsync(o => o.Id == id);

                if (order is null || order.CheckerId is null) return;

                await UpdateCheckerStatusAsync(id, OrderStatuses.Approved, order.CheckerId.Value);

                //var currentDesign = order.OrderDesigns.Last();
                //var container = blobServiceClient.GetBlobContainerClient(order.ArtistId.ToString());
                //var blob = container.GetBlobClient(currentDesign.Name);
                //var response = await blob.DownloadContentAsync();
                //var result = response.Value;
                //var contentType = result.Details.ContentType;
                //var contentStream = result.Content.ToStream();

                var smtpConfig = config.GetSection("Smtp");
                var client = new SmtpClient(smtpConfig["Host"], Convert.ToInt32(smtpConfig["Port"]))
                {
                    Credentials = new NetworkCredential(smtpConfig["Username"], smtpConfig["Password"]),
                    EnableSsl = true
                };
                var from = new MailAddress(smtpConfig["SenderEmail"]!, smtpConfig["SenderName"]);
                var to = new MailAddress(order.CustomerEmail);
                var message = new MailMessage(from, to);
                message.Body = $"Dear Customer,\r\n\r\nHere is the proof of your order {config["FrontendUrl"]}/order-proof/{order.Id}.\r\nPlease let us know if you'd like any changes! Thank you so much for your order, We hope your custom artwork! 😄\r\n\r\nBest Regards,\r\nTeam CP";
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = false;
                message.Subject = "Order Proof Ready";
                await client.SendMailAsync(message);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AssignArtistAsync(int id, Guid artistId)
        {
            try
            {
                var order = await dbContext.Orders
                    .Where(o => o.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(o => o.ArtistId, artistId)
                        .SetProperty(o => o.ArtistStatus, OrderStatuses.Claimed)
                        .SetProperty(o => o.UpdatedBy, artistId)
                        .SetProperty(o => o.UpdatedOn, DateTime.UtcNow));

                await CreateOrderHistory(id, OrderStatuses.Claimed, artistId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AssignCheckerAsync(int id, Guid checkerId)
        {
            try
            {
                var order = await dbContext.Orders
                    .Include(o => o.OrderDesigns)
                    .SingleOrDefaultAsync(o => o.Id == id);

                if (order is null) return;

                var status = order.OrderDesigns.Count > 0 ? OrderStatuses.ForReview : OrderStatuses.Claimed;

                order.CheckerId = checkerId;
                order.CheckerStatus = status;
                order.UpdatedBy = checkerId;
                order.UpdatedOn = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();

                await CreateOrderHistory(id, status, checkerId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateAsync(Order order)
        {
            try
            {
                await dbContext.Orders.AddAsync(order);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                await dbContext.Orders.Where(o => o.Id == id).ExecuteDeleteAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<GetOrdersModel>> GetAsync(bool? priority, string? artistStatus, string? checkerStatus, Guid? artistId, Guid? checkerId, bool noArtist = false, bool noChecker = false)
        {
            try
            {
                IQueryable<Order> queryableOrders = dbContext.Orders.Where(o => true);

                if (priority != null)
                {
                    queryableOrders = queryableOrders.Where(o => o.Priority == priority);
                }

                // for artists
                if (artistId != null)
                {
                    queryableOrders = queryableOrders.Where(o => o.ArtistId == artistId);
                } 
                else if (noArtist)
                {
                    queryableOrders = queryableOrders.Where(o => o.ArtistId == null);
                }

                // for checkers
                if (checkerId != null)
                {
                    queryableOrders = queryableOrders.Where(o => o.CheckerId == checkerId);
                }
                else if (noChecker)
                {
                    queryableOrders = queryableOrders.Where(o => o.CheckerId == null);
                }

                // order status
                if (!string.IsNullOrEmpty(artistStatus))
                {
                    var statuses = artistStatus.Split(',');
                    queryableOrders = queryableOrders.Where(o => statuses.Contains(o.ArtistStatus));
                }
                if (!string.IsNullOrEmpty(checkerStatus))
                {
                    var statuses = checkerStatus.Split(',');
                    queryableOrders = queryableOrders.Where(o => statuses.Contains(o.CheckerStatus));
                }

                var orders = await queryableOrders.OrderByDescending(o => o.Priority).ThenBy(o => o.CreatedOn).ToListAsync();
                var result = orders.AsGetOrdersModel();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<GetOrderModel> GetByIdAsync(int id)
        {
            try
            {
                var order = await dbContext.Orders
                    .Include(o => o.DesignBracket)
                    .Include(o => o.PrintColor)
                    .Include(o => o.OutputSize)
                    .Include(o => o.OrderImageReferences)
                    .Include(o => o.OrderDesigns).ThenInclude(od => od.OrderDesignComments)
                    .SingleAsync(o => o.Id == id);

                var designs = new List<DesignModel>();

                if (order.OrderDesigns.Any())
                {
                    var container = blobServiceClient.GetBlobContainerClient(order.ArtistId.ToString());

                    foreach (var orderDesign in order.OrderDesigns)
                    {
                        var blob = container.GetBlobClient(orderDesign.Name);
                        var url = blob.GenerateSasUri(BlobSasPermissions.Read, DateTimeOffset.UtcNow.AddDays(1)).ToString();
                        var design = orderDesign.AsModel(url);
                        designs.Add(design);
                    }
                }

                var result = order.AsGetOrderModel(designs);

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SubmitDesignAsync(int id, string designName, Stream designContent)
        {
            try
            {
                var order = await dbContext.Orders
                    .Include(o => o.OrderDesigns)
                    .SingleAsync(o => o.Id == id);

                if (order is null || order.ArtistId is null) return;

                var container = blobServiceClient.GetBlobContainerClient(order.ArtistId.ToString());
                await container.CreateIfNotExistsAsync();

                var saltedDesignName = SaltDesignName(designName);

                var blob = container.GetBlobClient(saltedDesignName);
                await blob.UploadAsync(designContent);

                order.OrderDesigns.Add(new OrderDesign
                {
                    OrderId = order.Id,
                    Name = saltedDesignName,
                    CreatedBy = order.ArtistId.Value,
                });

                await dbContext.SaveChangesAsync();

                await UpdateArtistStatusAsync(id, OrderStatuses.DesignSubmitted, order.ArtistId.Value);

                if (order.CheckerId is null) return;

                await UpdateCheckerStatusAsync(id, OrderStatuses.ForReview, order.CheckerId.Value);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateAsync(Order order)
        {
            try
            {
                var currentOrder = await dbContext.Orders.Include(o => o.OrderImageReferences).SingleOrDefaultAsync(o => o.Id == order.Id);

                if (currentOrder is null) return;

                currentOrder.OrderNumber = order.OrderNumber;
                currentOrder.Priority = order.Priority;
                currentOrder.Concept = order.Concept;
                currentOrder.PrintColorId = order.PrintColorId;
                currentOrder.DesignBracketId = order.DesignBracketId;
                currentOrder.OutputSizeId = order.OutputSizeId;
                currentOrder.CustomerEmail = order.CustomerEmail;
                currentOrder.UpdatedBy = order.UpdatedBy;
                currentOrder.UpdatedOn = order.UpdatedOn;

                currentOrder.OrderImageReferences.Clear();
                foreach (var imageRef in order.OrderImageReferences)
                {
                    currentOrder.OrderImageReferences.Add(imageRef);
                }

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateArtistStatusAsync(int id, string status, Guid artistId)
        {
            try
            {
                await dbContext.Orders
                    .Where(o => o.Id == id)
                    .ExecuteUpdateAsync(setter => setter
                        .SetProperty(o => o.ArtistStatus, status)
                        .SetProperty(o => o.UpdatedBy, artistId)
                        .SetProperty(o => o.UpdatedOn, DateTime.UtcNow));

                await CreateOrderHistory(id, status, artistId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task UpdateCheckerStatusAsync(int id, string status, Guid checkerId)
        {
            try
            {
                await dbContext.Orders
                    .Where(o => o.Id == id)
                    .ExecuteUpdateAsync(setter => setter
                        .SetProperty(o => o.CheckerStatus, status)
                        .SetProperty(o => o.UpdatedBy, checkerId)
                        .SetProperty(o => o.UpdatedOn, DateTime.UtcNow));

                await CreateOrderHistory(id, status, checkerId);
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task CreateOrderHistory(int orderId, string status, Guid userId)
        {
            try
            {
                await dbContext.OrderStatusHistories.AddAsync(new OrderStatusHistory
                {
                    OrderId = orderId,
                    Status = status,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow,
                });

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        private string SaltDesignName(string designName)
        {
            var lastDotIndex = designName.LastIndexOf('.');
            var fileName  = designName.Substring(0, lastDotIndex);
            var fileExtension = designName.Substring(lastDotIndex);
            var result = $"{fileName}_{Guid.NewGuid().ToString()[..5]}{fileExtension}";

            if (result.Length > 100) result = result.Substring(result.Length - 100); // max length is 100

            return result;
        }
    }
}
