using CottonPrompt.Infrastructure.Models.DesignBrackets;

namespace CottonPrompt.Infrastructure.Services.DesignBrackets
{
    public interface IDesignBracketService
    {
        Task<IEnumerable<DesignBracket>> GetAsync();

        Task SwapAsync(int id1, int id2);

        Task UpdateAsync(int id, string value);

        Task<GetOrdersCountModel> GetOrdersCountAsync(int id);

        Task DeleteAsync(int id);
    }
}
