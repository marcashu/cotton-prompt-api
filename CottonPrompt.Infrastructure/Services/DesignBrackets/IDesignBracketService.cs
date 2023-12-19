using CottonPrompt.Infrastructure.Models.DesignBrackets;

namespace CottonPrompt.Infrastructure.Services.DesignBrackets
{
    public interface IDesignBracketService
    {
        Task<IEnumerable<DesignBracket>> GetAsync();
    }
}
