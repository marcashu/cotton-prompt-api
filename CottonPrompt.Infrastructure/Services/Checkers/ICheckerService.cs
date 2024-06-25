using CottonPrompt.Infrastructure.Models;

namespace CottonPrompt.Infrastructure.Services.Checkers
{
    public interface ICheckerService
    {
        Task<CanDoModel> CanClaimTrainingGroupAsync(Guid id);
    }
}
