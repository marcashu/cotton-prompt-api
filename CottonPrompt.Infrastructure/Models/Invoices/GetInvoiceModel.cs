namespace CottonPrompt.Infrastructure.Models.Invoices
{
    public record GetInvoiceModel(
        Guid Id,
        DateTime EndDate,
        decimal Amount,
        IEnumerable<GetInvoiceSectionModel> Sections
    );
}
