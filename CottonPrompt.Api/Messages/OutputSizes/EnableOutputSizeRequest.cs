using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.OutputSizes
{
    public class EnableOutputSizeRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
