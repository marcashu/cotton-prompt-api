using Microsoft.AspNetCore.Mvc;

namespace CottonPrompt.Api.Messages.Orders
{
    public class GetReportedOrdersRequest
    {
        [FromQuery(Name = "orderNumber")]
        public string? OrderNumber { get; set; }
    }
}
