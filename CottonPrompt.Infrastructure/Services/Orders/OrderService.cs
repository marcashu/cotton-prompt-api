using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Messages.Orders;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public class OrderService(CottonPromptContext dbContext) : IOrderService
    {
        public async Task CreateAsync(CreateOrderRequest request)
        {
            try
            {
                var order = request.AsEntity();
                await dbContext.Orders.AddAsync(order);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        } 

        public async Task<IEnumerable<Models.Orders.Order>> GetAsync()
        {
            try
            {
                var orders = await dbContext.Orders
                    .Include(o => o.DesignBracket)
                    .Include(o => o.OrderImageReferences)
                    .ToListAsync();
                var result = orders.AsModel();
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
