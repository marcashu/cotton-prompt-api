using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public class OrderService(CottonPromptContext dbContext) : IOrderService
    {
        public async Task AssignArtistAsync(int id, Guid artistId)
        {
            try
            {
                var order = await dbContext.Orders.FindAsync(id);

                if (order is null) return;

                order.ArtistClaimedBy = artistId;
                order.ArtistClaimedOn = DateTime.UtcNow;
                order.UpdatedBy = artistId;
                order.UpdatedOn = DateTime.UtcNow;
                await dbContext.SaveChangesAsync();
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

        public async Task<IEnumerable<GetOrdersModel>> GetAsync(bool priority, Guid? artistId, bool hasArtistFilter = false)
        {
            try
            {
                var orders = await dbContext.Orders
                    .Where(o => o.Priority == priority
                        && (!hasArtistFilter || (hasArtistFilter && o.ArtistClaimedBy == artistId)))
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
                    .SingleAsync(o => o.Id == id);
                var result = order.AsGetOrderModel();
                return result;
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
