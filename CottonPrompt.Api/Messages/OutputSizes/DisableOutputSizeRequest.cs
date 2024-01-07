using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.OutputSizes
{
    public class DisableOutputSizeRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
