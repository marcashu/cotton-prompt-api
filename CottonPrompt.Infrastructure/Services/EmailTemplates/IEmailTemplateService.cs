using CottonPrompt.Infrastructure.Models.Rates;

namespace CottonPrompt.Infrastructure.Services.EmailTemplates
{
    public interface IEmailTemplateService
    {
        Task<EmailTemplatesModel> GetAsync();

        Task UpdateOrderReadyAsync(string content);
    }
}
