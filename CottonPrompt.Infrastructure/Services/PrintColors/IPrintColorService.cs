using CottonPrompt.Infrastructure.Models.PrintColors;

namespace CottonPrompt.Infrastructure.Services.PrintColors
{
    public interface IPrintColorService
    {
        Task<IEnumerable<PrintColor>> GetAsync(bool hasActiveFilter, bool active);

        Task SwapAsync(int id1, int id2, Guid userId);

        Task UpdateAsync(int id, string value, Guid userId);

        Task<GetPrintColorOrdersCountModel> GetOrdersCountAsync(int id);

        Task DeleteAsync(int id);

        Task DisableAsync(int id, Guid userId);

        Task EnableAsync(int id, Guid userId);

        Task CreateAsync(string value, Guid userId);
    }
}
