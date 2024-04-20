using Microsoft.AspNetCore.Mvc;

namespace CottonPrompt.Api.Messages.Orders
{
    public class GetRejectedOrdersRequest
    {
        [FromQuery(Name = "orderNumber")]
        public string? OrderNumber { get; set; }
    }
}
