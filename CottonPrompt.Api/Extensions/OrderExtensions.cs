using CottonPrompt.Api.Messages.Orders;
using CottonPrompt.Infrastructure.Entities;

namespace CottonPrompt.Api.Extensions
{
    public static class OrderExtensions
    {
        public static Order AsEntity(this CreateOrderRequest request)
        {
            var result = new Order
            {
                OrderNumber = request.OrderNumber,
                Priority = request.Priority,
                Concept = request.Concept,
                PrintColor = request.PrintColor,
                DesignBracketId = request.DesignBracketId,
                OrderImageReferences = request.ImageReferences?.Select((r, i) => new OrderImageReference
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
