using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.Design;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class DesignExtensions
    {
        internal static DesignModel AsModel(this OrderDesign entity)
        {
            var result = new DesignModel(entity.LineId, entity.Name, string.Empty, entity.CreatedOn);
            return result;
        }

        internal static IEnumerable<DesignModel> AsModel(this IEnumerable<OrderDesign> entities)
        {
            var result = entities.Select(AsModel);
            return result;
        }
    }
}
