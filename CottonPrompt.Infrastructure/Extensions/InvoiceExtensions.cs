using CottonPrompt.Infrastructure.Constants;
using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.Invoices;

namespace CottonPrompt.Infrastructure.Extensions
{
    internal static class InvoiceExtensions
    {
        internal static GetInvoicesModel AsGetInvoicesModel(this Invoice entity)
        {
            var phTimeOffset = 8;
            var status = DateTime.UtcNow.AddHours(phTimeOffset) > entity.EndDate ? InvoiceStatuses.Completed : InvoiceStatuses.Ongoing;
            var result = new GetInvoicesModel(entity.Id, entity.StartDate, entity.EndDate, entity.Amount, status);
            return result;
        }

        internal static IEnumerable<GetInvoicesModel> AsGetInvoicesModel(this IEnumerable<Invoice> entities) 
        {
            var result = entities.Select(AsGetInvoicesModel);
            return result;
        }
    }
}
