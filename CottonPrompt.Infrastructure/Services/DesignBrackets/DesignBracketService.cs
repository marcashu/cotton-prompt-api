using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.DesignBrackets;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.DesignBrackets
{
    public class DesignBracketService(CottonPromptContext dbContext) : IDesignBracketService
    {
        public async Task<IEnumerable<DesignBracket>> GetAsync()
        {
            try
            {
                var designBrackets = await dbContext.OrderDesignBrackets.ToListAsync();
                var result = designBrackets.AsModel();
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
