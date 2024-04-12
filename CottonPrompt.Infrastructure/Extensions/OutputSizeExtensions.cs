using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.OutputSizes;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class OutputSizeExtensions
    {
        internal static OutputSize AsModel(this OrderOutputSize entity)
        {
            var result = new OutputSize(entity.Id, entity.Value);
            return result;
        }

        internal static IEnumerable<OutputSize> AsModel(this IEnumerable<OrderOutputSize> entities) 
        {
            var result = entities.Select(AsModel); 
            return result;
        }
    }
}
