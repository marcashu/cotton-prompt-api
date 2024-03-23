namespace CottonPrompt.Infrastructure.Models.Invoices
{
    public record GetInvoiceSectionModel(
        string Name,
        decimal Rate,
        decimal Amount,
        int Quantity,
        IEnumerable<string> OrderNumbers
    );
}
