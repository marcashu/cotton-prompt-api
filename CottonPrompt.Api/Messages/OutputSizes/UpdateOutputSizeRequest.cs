using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.OutputSizes
{
    public class UpdateOutputSizeRequest
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
