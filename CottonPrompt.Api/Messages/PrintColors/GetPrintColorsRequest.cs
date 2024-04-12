using Microsoft.AspNetCore.Mvc;

namespace CottonPrompt.Api.Messages.PrintColors
{
    public class GetPrintColorsRequest
    {
        [FromQuery(Name = "hasActiveFilter")]
        public bool HasActiveFilter { get; set; }

        [FromQuery(Name = "active")]
        public bool Active { get; set; }
    }
}
