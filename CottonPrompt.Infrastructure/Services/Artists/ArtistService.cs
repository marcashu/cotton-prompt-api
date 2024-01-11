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
				
				if (forReuploadOrders > 2)
				{
					return new CanArtistClaimModel(false, "You can't claim a new order because you already have 3 orders for reupload.");
				}

				if (newlyClaimedOrders > 0)
				{
					return new CanArtistClaimModel(false, "You can't claim a new order because you already have 1 order for upload.");
				}
				
				return new CanArtistClaimModel(true, string.Empty);
            }
			catch (Exception)
			{
				throw;
			}
        }
    }
}
