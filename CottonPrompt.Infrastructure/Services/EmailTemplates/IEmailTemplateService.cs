using CottonPrompt.Infrastructure.Models.EmailTemplates;

namespace CottonPrompt.Infrastructure.Services.EmailTemplates
{
    public interface IEmailTemplateService
    {
        Task<EmailTemplatesModel> GetAsync();

        Task UpdateOrderReadyAsync(string content);
    }
}
