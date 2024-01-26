using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using CottonPrompt.Infrastructure.Constants;
using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.Designs;
using CottonPrompt.Infrastructure.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public class OrderService(CottonPromptContext dbContext, BlobServiceClient blobServiceClient) : IOrderService
    {
        public async Task ApproveAsync(int id)
        {
            try
            {
                var order = await dbContext.Orders.FindAsync(id);

                if (order is null || order.CheckerId is null) return;

                await UpdateCheckerStatusAsync(id, OrderStatuses.Approved, order.CheckerId.Value);
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

                var saltedDesignName = $"{designName}_{Guid.NewGuid()}";

                if (saltedDesignName.Length > 100) saltedDesignName = saltedDesignName[..100]; // max length is 100

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
    }
}
