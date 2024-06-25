using Microsoft.AspNetCore.Mvc;

namespace CottonPrompt.Api.Messages.Orders
{
    public class GetAvailableAsCheckerOrdersRequest
    {
        [FromQuery(Name = "priority")]
        public bool? Priority { get; set; }

        [FromQuery(Name = "trainingGroup")]
        public bool TrainingGroup { get; set; }
    }
}
