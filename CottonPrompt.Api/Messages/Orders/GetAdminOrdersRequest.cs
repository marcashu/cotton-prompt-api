using Microsoft.AspNetCore.Mvc;

namespace CottonPrompt.Api.Messages.Orders
{
    public class GetAdminOrdersRequest
    {
        [FromQuery(Name = "orderNumbers")]
        public string? OrderNumbers { get; set; }

        [FromQuery(Name = "priorities")]
        public string? Priorities { get; set; }

        [FromQuery(Name = "artists")]
        public string? Artists { get; set; }

        [FromQuery(Name = "checkers")]
        public string? Checkers { get; set; }

        [FromQuery(Name = "customers")]
        public string? Customers { get; set; }

        [FromQuery(Name = "status")]
        public string? Status { get; set; }

        [FromQuery(Name = "userGroups")]
        public string? UserGroups { get; set; }
    }
}
