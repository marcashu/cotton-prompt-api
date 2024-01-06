using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.PrintColors;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class PrintColorExtensions
    {
        internal static PrintColor AsModel(this OrderPrintColor entity)
        {
            var result = new PrintColor(entity.Id, entity.Value);
            return result;
        }

        internal static IEnumerable<PrintColor> AsModel(this IEnumerable<OrderPrintColor> entities) 
        {
            var result = entities.Select(AsModel); 
            return result;
        }
    }
}
