using CottonPrompt.Infrastructure.Models.Invoices;

namespace CottonPrompt.Infrastructure.Services.Invoices
{
    public interface IInvoiceService
    {
        Task<IEnumerable<GetInvoicesModel>> GetAsync(Guid userId);
    }
}
