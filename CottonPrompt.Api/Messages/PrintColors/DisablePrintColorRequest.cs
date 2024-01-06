using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.PrintColors
{
    public class DisablePrintColorRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
