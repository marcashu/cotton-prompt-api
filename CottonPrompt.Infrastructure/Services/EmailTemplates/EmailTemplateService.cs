using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Extensions;
using CottonPrompt.Infrastructure.Models.EmailTemplates;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.EmailTemplates
{
    public class EmailTemplateService(CottonPromptContext dbContext) : IEmailTemplateService
    {
        public async Task<EmailTemplatesModel> GetAsync()
        {
			try
			{
				var emailTemplate = await dbContext.EmailTemplates.FirstAsync();
				var result = emailTemplate.AsModel();
				return result;
			}
			catch (Exception)
			{
				throw;
			}
        }

        public async Task UpdateOrderReadyAsync(string content)
        {
			try
			{
				await dbContext.EmailTemplates.ExecuteUpdateAsync(et => et.SetProperty(x => x.OrderProofReadyEmail, content));
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
