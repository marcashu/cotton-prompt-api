using Microsoft.AspNetCore.Mvc;

namespace CottonPrompt.Api.Messages.DesignBrackets
{
    public class GetInvoicesRequest
    {
        [FromQuery(Name = "userId")]
        public Guid UserId { get; set; }
    }
}
