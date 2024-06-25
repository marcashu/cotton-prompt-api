using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.Checkers
{
    public class CheckerService(CottonPromptContext dbContext) : ICheckerService
    {
        public async Task<CanDoModel> CanClaimTrainingGroupAsync(Guid id)
        {
			try
			{
                var trainingGroupCheckersId = await dbContext.Settings.Select(s => s.TrainingGroupCheckersGroupId).FirstOrDefaultAsync();
                var isTrainingGroupChecker = await dbContext.UserGroupUsers.AnyAsync(ugu => ugu.UserId == id && ugu.UserGroupId == trainingGroupCheckersId);
                return new CanDoModel(isTrainingGroupChecker, string.Empty);
            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}
