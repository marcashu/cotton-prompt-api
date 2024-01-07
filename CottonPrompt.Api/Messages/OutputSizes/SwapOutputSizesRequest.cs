using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.OutputSizes
{
    public class SwapOutputSizesRequest
    {
        [Required]
        public int Id1 { get; set; }

        [Required]
        public int Id2 { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
