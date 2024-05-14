namespace CottonPrompt.Infrastructure.Models.Orders
{
    public record GetOrdersModel(
        int Id, 
        string OrderNumber, 
        bool Priority,
        DateTime Date,
        string ArtistStatus,
        string CheckerStatus,
        Guid? ArtistId,
        string? ArtistName,
        string? CheckerName,
        string CustomerStatus,
        string CustomerName,
        int? OriginalOrderId,
        int? ChangeRequestOrderId,
        string? Reason,
        DateTime? AcceptedOn,
        DateTime? ChangeRequestedOn,
        DateTime? ReportedOn,
        string? ReporterName,
        DateTime? SentForPrintingOn
    );
}
