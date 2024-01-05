using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.DesignBrackets
{
    public class SwapRequest
    {
        [Required]
        public int Id1 { get; set; }

        [Required]
        public int Id2 { get; set; }
    }
}
