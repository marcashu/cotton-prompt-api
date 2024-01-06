using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class CreateOrderRequest
    {
        public string OrderNumber { get; set; }

        public bool Priority { get; set; }

        public string Concept { get; set; }

        public int PrintColorId { get; set; }

        public int DesignBracketId { get; set; }

        public IEnumerable<string>? ImageReferences { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
