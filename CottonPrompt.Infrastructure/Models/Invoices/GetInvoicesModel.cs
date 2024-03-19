namespace CottonPrompt.Infrastructure.Models.Invoices
{
    public record GetInvoicesModel(
        Guid Id,
        DateTime StartDate,
        DateTime EndDate,
        decimal Amount,
        string Status
    );
}
