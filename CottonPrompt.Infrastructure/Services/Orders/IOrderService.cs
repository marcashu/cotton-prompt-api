using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.Orders;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<GetOrdersModel>> GetAsync(bool priority, Guid? artistId, bool hasArtistFilter = false);

        Task<GetOrderModel> GetByIdAsync(int id);

        Task CreateAsync(Order order);

        Task UpdateAsync(Order order);

        Task DeleteAsync(int id);

        Task AssignArtistAsync(int id, Guid artistId);
    }
}
