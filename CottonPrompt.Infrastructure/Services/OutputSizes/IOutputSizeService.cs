using CottonPrompt.Infrastructure.Models.OutputSizes;

namespace CottonPrompt.Infrastructure.Services.OutputSizes
{
    public interface IOutputSizeService
    {
        Task<IEnumerable<OutputSize>> GetAsync(bool hasActiveFilter, bool active);

        Task SwapAsync(int id1, int id2, Guid userId);

        Task UpdateAsync(int id, string value, Guid userId);

        Task<GetOutputSizeOrdersCountModel> GetOrdersCountAsync(int id);

        Task DeleteAsync(int id);

        Task DisableAsync(int id, Guid userId);

        Task EnableAsync(int id, Guid userId);

        Task CreateAsync(string value, Guid userId);
    }
}
