using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.PrintColors
{
    public class UpdatePrintColorRequest
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
