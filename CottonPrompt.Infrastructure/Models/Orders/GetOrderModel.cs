using CottonPrompt.Infrastructure.Models.DesignBrackets;
using CottonPrompt.Infrastructure.Models.Designs;

namespace CottonPrompt.Infrastructure.Models.Orders
{
    public record GetOrderModel(
        int Id,
        string OrderNumber,
        bool Priority,
        string Concept,
        string PrintColor,
        DesignBracket DesignBracket,
        IEnumerable<string> ImageReferences,
        DesignModel? Design,
        IEnumerable<DesignModel> PreviousDesigns
    );
}
