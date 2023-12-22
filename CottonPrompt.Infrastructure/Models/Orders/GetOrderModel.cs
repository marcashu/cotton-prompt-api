using CottonPrompt.Infrastructure.Models.DesignBrackets;

namespace CottonPrompt.Infrastructure.Models.Orders
{
    public record GetOrderModel(
        int Id,
        string OrderNumber,
        bool Priority,
        string Concept,
        string PrintColor,
        DesignBracket DesignBracket,
        IEnumerable<string> ImageReferences
    );
}
