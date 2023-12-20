using CottonPrompt.Infrastructure.Entities;
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

        internal static GetOrderModel AsGetOrderModel(this Order entity)
        {
            var result = new GetOrderModel(entity.Id, entity.OrderNumber, entity.Priority, entity.Concept, entity.PrintColor, entity.DesignBracketId, entity.OrderImageReferences.Select(oir => oir.Url));
            return result;
        }
    }
}
