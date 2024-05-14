using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class SendForPrintingRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
