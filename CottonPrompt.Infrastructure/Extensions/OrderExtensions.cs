using CottonPrompt.Infrastructure.Models.Orders;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class OrderExtensions
    {
        internal static GetOrdersModel AsGetOrdersModel(this Entities.Order entity)
        {
            var result = new GetOrdersModel(entity.Id, entity.OrderNumber, entity.CreatedOn);
            return result;
        }

        internal static IEnumerable<GetOrdersModel> AsGetOrdersModel(this IEnumerable<Entities.Order> entities)
        {
            var result = entities.Select(AsGetOrdersModel);
            return result;
        }
    }
}
