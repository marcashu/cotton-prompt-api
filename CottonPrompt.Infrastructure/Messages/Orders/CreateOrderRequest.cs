using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Infrastructure.Messages.Orders
{
    public class CreateOrderRequest
    {
        public string Number { get; set; }

        public bool IsPriority { get; set; }

        public string Concept { get; set; }

        public string PrintColor { get; set; }

        public int DesignBracketId { get; set; }

        public IEnumerable<string>? ImageReferences { get; set; }

        public Guid CreatedBy { get; set; }
    }
}
