namespace CottonPrompt.Infrastructure.Models.Orders
{
    public record GetOrderModel(
        int Id,
        string OrderNumber,
        bool Priority,
        string Concept,
        string PrintColor,
        int DesignBracketId,
        IEnumerable<string> ImageReferences
    );
}
