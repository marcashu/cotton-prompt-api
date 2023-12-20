using CottonPrompt.Infrastructure.Models.DesignBrackets;

namespace CottonPrompt.Infrastructure.Models.Orders
{
    public record GetOrdersModel(
        int Id, 
        string OrderNumber, 
        bool Priority,
        DateTime CreatedOn
    );
}
