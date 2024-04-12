using Microsoft.AspNetCore.Mvc;

namespace CottonPrompt.Api.Messages.OutputSizes
{
    public class GetOutputSizesRequest
    {
        [FromQuery(Name = "hasActiveFilter")]
        public bool HasActiveFilter { get; set; }

        [FromQuery(Name = "active")]
        public bool Active { get; set; }
    }
}
