using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models;
using CottonPrompt.Infrastructure.Models.Orders;

namespace CottonPrompt.Infrastructure.Services.Orders
{
    public interface IOrderService
    {
        Task<IEnumerable<GetOrdersModel>> GetAsync(bool? priority, string? artistStatus, string? checkerStatus, string? customerStatus, Guid? artistId, Guid? checkerId, bool noArtist = false, bool noChecker = false);

        Task<IEnumerable<GetOrdersModel>> GetOngoingAsync(OrderFiltersModel? filters = null);

        Task<IEnumerable<GetOrdersModel>> GetRejectedAsync(OrderFiltersModel? filters = null);

        Task<IEnumerable<GetOrdersModel>> GetCompletedAsync(OrderFiltersModel? filters = null);

        Task<IEnumerable<GetOrdersModel>> GetReportedAsync(OrderFiltersModel? filters = null);

        Task<IEnumerable<GetOrdersModel>> GetSentForPrintingAsync(OrderFiltersModel? filters = null);

        Task<IEnumerable<GetOrdersModel>> GetAvailableAsArtistAsync(Guid artistId, bool? priority, bool changeRequest = false);

        Task<GetOrderModel> GetByIdAsync(int id);

        Task CreateAsync(Order order);

        Task UpdateAsync(Order order);

        Task DeleteAsync(int id);

        Task<CanDoModel> AssignArtistAsync(int id, Guid artistId);

        Task<CanDoModel> AssignCheckerAsync(int id, Guid checkerId);

        Task SubmitDesignAsync(int id, string designName, string designContent);

        Task ApproveAsync(int id);

        Task AcceptAsync(int id);

        Task ChangeRequestAsync(int id, int designId, string comment, IEnumerable<OrderImageReference> imageReferences);

        Task<DownloadModel> DownloadAsync(int id);

        Task ResendForCustomerReviewAsync(int id);

        Task ReportAsync(int id, string reason, bool isRedraw);

        Task ResolveAsync(int id, Guid resolvedBy);

        Task SendForPrintingAsync(int id, Guid userId);
    }
}
