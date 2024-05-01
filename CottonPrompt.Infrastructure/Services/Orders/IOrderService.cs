using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models;
using CottonPrompt.Infrastructure.Models.Orders;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<GetOrdersModel>> GetAsync(bool? priority, string? artistStatus, string? checkerStatus, string? customerStatus, Guid? artistId, Guid? checkerId, bool noArtist = false, bool noChecker = false);

        Task<IEnumerable<GetOrdersModel>> GetOngoingAsync(string? orderNumber);

        Task<IEnumerable<GetOrdersModel>> GetRejectedAsync(string? orderNumber);

        Task<IEnumerable<GetOrdersModel>> GetCompletedAsync(string? orderNumber);

        Task<IEnumerable<GetOrdersModel>> GetAvailableAsArtistAsync(Guid artistId, bool? priority, bool changeRequest = false);

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

        Task<DownloadModel> DownloadAsync(int id);

        Task ResendForCustomerReviewAsync(int id);
    }
}
