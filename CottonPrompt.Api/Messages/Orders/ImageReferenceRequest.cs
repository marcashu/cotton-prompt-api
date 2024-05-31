using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class ImageReferenceRequest
    {
        [Required]
        public string Type { get; set; }

        [Required]
        public string Value { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
