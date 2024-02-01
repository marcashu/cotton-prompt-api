using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class CreateOrderRequest
    {
        [Required]
        public string OrderNumber { get; set; }

        [Required]
        public bool Priority { get; set; }

        [Required]
        public string Concept { get; set; }

        [Required]
        public int PrintColorId { get; set; }

        [Required]
        public int DesignBracketId { get; set; }

        [Required]
        public int OutputSizeId { get; set; }

        [Required]
        public string CustomerEmail { get; set; }

        public IEnumerable<string>? ImageReferences { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }
    }
}
