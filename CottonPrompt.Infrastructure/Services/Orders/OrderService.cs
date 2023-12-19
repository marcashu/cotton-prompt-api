using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.Orders;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public class OrderService(CottonPromptContext dbContext) : IOrderService
    {
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

        public async Task<IEnumerable<GetOrdersModel>> GetAsync(bool priority)
        {
            try
            {
                var orders = await dbContext.Orders
                    .Where(o => o.Priority == priority)
                    .Include(o => o.DesignBracket)
                    .Include(o => o.OrderImageReferences)
                    .OrderBy(o => o.CreatedOn)
                    .ToListAsync();
                var result = orders.AsGetOrdersModel();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
