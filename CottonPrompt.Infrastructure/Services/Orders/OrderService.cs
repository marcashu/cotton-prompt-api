using Azure.Storage.Blobs;
using Azure.Storage.Sas;
using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.Designs;
using CottonPrompt.Infrastructure.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public class OrderService(CottonPromptContext dbContext, BlobServiceClient blobServiceClient) : IOrderService
    {
        public async Task ApproveAsync(int id, Guid checkerId)
        {
            try
            {
                await dbContext.Orders
                    .Where(o => o.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(o => o.ApprovedBy, checkerId)
                        .SetProperty(o => o.ApprovedOn, DateTime.UtcNow)
                        .SetProperty(o => o.UpdatedBy, checkerId)
                        .SetProperty(o => o.UpdatedOn, DateTime.UtcNow)
                        .SetProperty(o => o.CheckerStatus, "Approved"));
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
                        .SetProperty(o => o.ArtistClaimedBy, artistId)
                        .SetProperty(o => o.ArtistClaimedOn, DateTime.UtcNow)
                        .SetProperty(o => o.UpdatedBy, artistId)
                        .SetProperty(o => o.UpdatedOn, DateTime.UtcNow)
                        .SetProperty(o => o.ArtistStatus, "Claimed"));
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
                    .Where(o => o.Id == id)
                    .ExecuteUpdateAsync(setters => setters
                        .SetProperty(o => o.CheckerClaimedBy, checkerId)
                        .SetProperty(o => o.CheckerClaimedOn, DateTime.UtcNow)
                        .SetProperty(o => o.UpdatedBy, checkerId)
                        .SetProperty(o => o.UpdatedOn, DateTime.UtcNow)
                        .SetProperty(o => o.CheckerStatus, "Claimed"));
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

        public async Task<IEnumerable<GetOrdersModel>> GetAsync(bool priority, Guid? artistId, Guid? checkerId, bool hasArtistFilter = false, bool hasCheckerFilter = false)
        {
            try
            {
                var orders = await dbContext.Orders
                    .Where(o => o.Priority == priority
                        && (!hasArtistFilter || (hasArtistFilter && o.ArtistClaimedBy == artistId))
                        && (!hasCheckerFilter || (hasCheckerFilter && o.CheckerClaimedBy == checkerId)))
                    .OrderBy(o => o.CreatedOn)
                    .ToListAsync();
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
                    .Include(o => o.OrderImageReferences)
                    .Include(o => o.OrderDesigns).ThenInclude(od => od.OrderDesignComments)
                    .SingleAsync(o => o.Id == id);

                var designs = new List<DesignModel>();

                if (order.OrderDesigns.Any())
                {
                    var container = blobServiceClient.GetBlobContainerClient(order.ArtistClaimedBy.ToString());

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

                if (order is null || order.ArtistClaimedBy is null) return;

                var container = blobServiceClient.GetBlobContainerClient(order.ArtistClaimedBy.ToString());
                await container.CreateIfNotExistsAsync();

                var saltedDesignName = $"{designName}_{Guid.NewGuid()}";

                if (saltedDesignName.Length > 100) saltedDesignName = saltedDesignName[..100]; // max length is 100

                var blob = container.GetBlobClient(saltedDesignName);
                await blob.UploadAsync(designContent);

                order.OrderDesigns.Add(new OrderDesign
                {
                    OrderId = order.Id,
                    Name = saltedDesignName,
                    CreatedBy = order.ArtistClaimedBy.Value,
                });

                order.ArtistStatus = "Design submitted";
                order.UpdatedBy = order.ArtistClaimedBy;
                order.UpdatedOn = DateTime.UtcNow;

                await dbContext.SaveChangesAsync();
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
                currentOrder.PrintColor = order.PrintColor;
                currentOrder.DesignBracketId = order.DesignBracketId;
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
    }
}
