using CottonPrompt.Infrastructure.Models.DesignBrackets;

namespace CottonPrompt.Infrastructure.Models.Orders
{
    public record GetOrdersModel(
        int Id, 
        string OrderNumber, 
        bool Priority,
        DateTime CreatedOn,
        string ArtistStatus,
        string CheckerStatus,
        Guid? ArtistId,
        string? ArtistName,
        string? CheckerName,
        string CustomerStatus,
        string CustomerName,
        int? OriginalOrderId,
        int? ChangeRequestOrderId,
        string? Reason
    );
}
