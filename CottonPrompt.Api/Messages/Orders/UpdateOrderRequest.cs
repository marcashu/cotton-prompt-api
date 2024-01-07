namespace CottonPrompt.Api.Messages.Orders
{
    public class UpdateOrderRequest
    {
        public int Id { get; set; }

        public string OrderNumber { get; set; }

        public bool Priority { get; set; }

        public string Concept { get; set; }

        public int PrintColorId { get; set; }

        public int DesignBracketId { get; set; }

        public int OutputSizeId { get; set; }

        public IEnumerable<string>? ImageReferences { get; set; }

        public Guid UpdatedBy { get; set; }
    }
}
