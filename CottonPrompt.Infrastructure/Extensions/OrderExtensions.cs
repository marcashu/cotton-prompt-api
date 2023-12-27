using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.Designs;
using CottonPrompt.Infrastructure.Models.Orders;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class OrderExtensions
    {
        internal static GetOrdersModel AsGetOrdersModel(this Order entity)
        {
            var result = new GetOrdersModel(entity.Id, entity.OrderNumber, entity.Priority, entity.CreatedOn);
            return result;
        }

        internal static IEnumerable<GetOrdersModel> AsGetOrdersModel(this IEnumerable<Entities.Order> entities)
        {
            var result = entities.Select(AsGetOrdersModel);
            return result;
        }

        internal static GetOrderModel AsGetOrderModel(this Order entity, IEnumerable<DesignModel> designs)
        {
            var result = new GetOrderModel(entity.Id, entity.OrderNumber, entity.Priority, entity.Concept, entity.PrintColor, entity.DesignBracket.AsModel(), entity.OrderImageReferences.Select(oir => oir.Url), designs);
            return result;
        }
    }
}
