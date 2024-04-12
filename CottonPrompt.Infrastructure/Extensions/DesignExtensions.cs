using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.Designs;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class DesignExtensions
    {
        internal static DesignModel AsModel(this OrderDesign entity, string url = "")
        {
            var result = new DesignModel(entity.Id, entity.Name, url, entity.CreatedOn, entity.OrderDesignComments.AsModel());
            return result;
        }
    }
}
