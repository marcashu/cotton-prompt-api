using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.Orders;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<GetOrdersModel>> GetAsync(bool priority);

        Task CreateAsync(Order order);

        Task DeleteAsync(int id);
    }
}
