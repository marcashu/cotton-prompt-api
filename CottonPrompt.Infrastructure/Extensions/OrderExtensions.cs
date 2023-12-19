using CottonPrompt.Infrastructure.Messages.Orders;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class OrderExtensions
    {
        internal static Models.Orders.Order AsModel(this Entities.Order entity)
        {
            var result = new Models.Orders.Order(entity.Id, entity.Number, entity.IsPriority, entity.Concept, entity.PrintColor, entity.DesignBracket.AsModel(), entity.OrderImageReferences.Select(r => r.Url), entity.CreatedOn);
            return result;
        }

        internal static IEnumerable<Models.Orders.Order> AsModel(this IEnumerable<Entities.Order> entities)
        {
            var result = entities.Select(AsModel);
            return result;
        }

        internal static Entities.Order AsEntity(this CreateOrderRequest request)
        {
            var result = new Entities.Order
            {
                Number = request.Number,
                IsPriority = request.IsPriority,
                Concept = request.Concept,
                PrintColor = request.PrintColor,
                DesignBracketId = request.DesignBracketId,
                OrderImageReferences = request.ImageReferences?.Select((r, i) => new Entities.OrderImageReference
                {
                    LineId = i + 1,
                    Url = r,
                }).ToList(),
                CreatedBy = request.CreatedBy,
            };
            return result;
        }
    }
}
