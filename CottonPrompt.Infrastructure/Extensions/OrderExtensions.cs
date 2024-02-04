using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.Designs;
using CottonPrompt.Infrastructure.Models.Orders;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class OrderExtensions
    {
        internal static GetOrdersModel AsGetOrdersModel(this Order entity)
        {
            var result = new GetOrdersModel(entity.Id, entity.OrderNumber, entity.Priority, entity.CreatedOn, entity.ArtistStatus, entity.CheckerStatus, entity.ArtistId);
            return result;
        }

        internal static IEnumerable<GetOrdersModel> AsGetOrdersModel(this IEnumerable<Order> entities)
        {
            var result = entities.Select(AsGetOrdersModel);
            return result;
        }

        internal static GetOrderModel AsGetOrderModel(this Order entity, IEnumerable<DesignModel> designs)
        {
            var currentDesign = ((entity.OriginalOrderId == null && designs.Any()) || (entity.OriginalOrderId != null && designs.Count() > 1)) ? designs.Last() : null;
            var previousDesigns = designs.Where(d => currentDesign == null || d.Id != currentDesign.Id);
            var result = new GetOrderModel(entity.Id, entity.OrderNumber, entity.Priority, entity.Concept, entity.PrintColor.AsModel(), entity.DesignBracket.AsModel(), entity.OutputSize.AsModel(), entity.UserGroupId, entity.CustomerEmail, entity.OrderImageReferences.Select(oir => oir.Url), currentDesign, previousDesigns, entity.ArtistStatus, entity.CheckerStatus, entity.CustomerStatus, entity.ArtistId, entity.CheckerId);
            return result;
        }
    }
}
