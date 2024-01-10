using CottonPrompt.Infrastructure.Models.Artists;

namespace CottonPrompt.Infrastructure.Services.Artists
{
    public interface IArtistService
    {
        Task<CanArtistClaimModel> CanClaimAsync(Guid id);
    }
}
