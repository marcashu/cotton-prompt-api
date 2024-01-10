using CottonPrompt.Infrastructure.Constants;
using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models.Artists;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.Artists
{
    public class ArtistService(CottonPromptContext dbContext) : IArtistService
    {
        public async Task<CanArtistClaimModel> CanClaimAsync(Guid id)
        {
			try
			{
				var forReuploadOrders = await dbContext.Orders.CountAsync(o => o.ArtistId == id && o.ArtistStatus == OrderStatuses.ForReupload);
				var newlyClaimedOrders = await dbContext.Orders.CountAsync(o => o.ArtistId == id && o.ArtistStatus == OrderStatuses.Claimed);
				var canClaim = forReuploadOrders < 3 && newlyClaimedOrders == 0;
				var result = new CanArtistClaimModel(canClaim);
				return result;
            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}
