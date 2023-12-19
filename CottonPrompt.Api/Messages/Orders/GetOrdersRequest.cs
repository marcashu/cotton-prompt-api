using Microsoft.AspNetCore.Mvc;

namespace CottonPrompt.Api.Messages.Orders
{
    public class GetOrdersRequest
    {
        [FromQuery(Name = "priority")]
        public bool Priority { get; set; }
    }
}
