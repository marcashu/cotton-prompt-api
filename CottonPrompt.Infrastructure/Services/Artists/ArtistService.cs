using CottonPrompt.Infrastructure.Constants;
using CottonPrompt.Infrastructure.Entities;
using CottonPrompt.Infrastructure.Models;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.Artists
{
    public class ArtistService(CottonPromptContext dbContext) : IArtistService
    {
        public async Task<CanDoModel> CanClaimAsync(Guid id)
        {
			try
			{
				var trainingGroupArtistsGroupId = await dbContext.Settings.Select(s => s.TrainingGroupArtistsGroupId).FirstOrDefaultAsync();
				var userGroups = await dbContext.UserGroupUsers.Where(ugu => ugu.UserId == id).Select(ugu => ugu.UserGroup).ToListAsync();
				var isTrainingGroupArtist = userGroups.Count == 1 && userGroups.First().Id == trainingGroupArtistsGroupId;

				if (isTrainingGroupArtist)
				{
                    var activeOrders = await dbContext.Orders.CountAsync(o => o.ArtistId == id && o.CheckerStatus != OrderStatuses.Approved);

                    if (activeOrders > 0)
                    {
                        return new CanDoModel(false, "You can't claim another order until the checker approves your current active order.");
                    }

                    return new CanDoModel(true, string.Empty);
                }
				else
                {
                    var forReuploadOrders = await dbContext.Orders.CountAsync(o => o.ArtistId == id && o.ArtistStatus == OrderStatuses.ForReupload);
                    var newlyClaimedOrders = await dbContext.Orders.CountAsync(o => o.ArtistId == id && o.ArtistStatus == OrderStatuses.Claimed);

                    if (forReuploadOrders > 2)
                    {
                        return new CanDoModel(false, "You can't claim a new order because you already have 3 orders for reupload.");
                    }

                    if (newlyClaimedOrders > 0)
                    {
                        return new CanDoModel(false, "You can't claim a new order because you already have 1 order for upload.");
                    }

                    return new CanDoModel(true, string.Empty);
                }
            }
			catch (Exception)
			{
				throw;
			}
        }

        public async Task<CanDoModel> CanClaimChangeRequestAsync(Guid id)
        {
			try
			{
				var changeRequestArtistsGroupId = await dbContext.Settings.Select(s => s.ChangeRequestArtistsGroupId).FirstOrDefaultAsync();
				var isChangeRequestArtist = await dbContext.UserGroupUsers.AnyAsync(ugu => ugu.UserId == id && ugu.UserGroupId == changeRequestArtistsGroupId);
				return new CanDoModel(isChangeRequestArtist, string.Empty);
			}
			catch (Exception)
			{
				throw;
			}
        }
    }
}
