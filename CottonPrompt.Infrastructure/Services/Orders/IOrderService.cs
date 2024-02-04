using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.Orders;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<GetOrdersModel>> GetAsync(bool? priority, string? artistStatus, string? checkerStatus, Guid? artistId, Guid? checkerId, bool noArtist = false, bool noChecker = false);

        Task<GetOrderModel> GetByIdAsync(int id);

        Task CreateAsync(Order order);

        Task UpdateAsync(Order order);

        Task DeleteAsync(int id);

        Task AssignArtistAsync(int id, Guid artistId);

        Task AssignCheckerAsync(int id, Guid checkerId);

        Task SubmitDesignAsync(int id, string designName, Stream designContent);

        Task ApproveAsync(int id);

        Task AcceptAsync(int id);

        Task ChangeRequestAsync(int id, int designId, string comment, IEnumerable<string> imageReferences);
    }
}
