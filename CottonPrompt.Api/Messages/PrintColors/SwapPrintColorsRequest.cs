using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.PrintColors
{
    public class SwapPrintColorsRequest
    {
        [Required]
        public int Id1 { get; set; }

        [Required]
        public int Id2 { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
