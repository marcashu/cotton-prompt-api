using CottonPrompt.Infrastructure.Models;

namespace CottonPrompt.Infrastructure.Services.Artists
{
    public interface IArtistService
    {
        Task<CanDoModel> CanClaimAsync(Guid id);

        Task<CanDoModel> CanClaimChangeRequestAsync(Guid id);
    }
}
