using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.DesignBrackets;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class DesignBracketExtensions
    {
        internal static DesignBracket AsModel(this OrderDesignBracket entity)
        {
            var result = new DesignBracket(entity.Id, entity.Value);
            return result;
        }

        internal static IEnumerable<DesignBracket> AsModel(this IEnumerable<OrderDesignBracket> entities) 
        {
            var result = entities.Select(AsModel); 
            return result;
        }
    }
}
