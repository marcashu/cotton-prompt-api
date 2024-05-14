using Microsoft.AspNetCore.Mvc;

namespace CottonPrompt.Api.Messages.Orders
{
    public class GetSentForPrintingOrdersRequest
    {
        [FromQuery(Name = "orderNumber")]
        public string? OrderNumber { get; set; }
    }
}
