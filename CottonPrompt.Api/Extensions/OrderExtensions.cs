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
                PrintColorId = request.PrintColorId,
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

        public static Order AsEntity(this UpdateOrderRequest request)
        {
            var result = new Order
            {
                Id = request.Id,
                OrderNumber = request.OrderNumber,
                Priority = request.Priority,
                Concept = request.Concept,
                PrintColorId = request.PrintColorId,
                DesignBracketId = request.DesignBracketId,
                OrderImageReferences = request.ImageReferences?.Select((r, i) => new OrderImageReference
                {
                    OrderId = request.Id,
                    LineId = i + 1,
                    Url = r,
                }).ToList(),
                UpdatedBy = request.UpdatedBy,
                UpdatedOn = DateTime.UtcNow,
            };
            return result;
        }
    }
}
