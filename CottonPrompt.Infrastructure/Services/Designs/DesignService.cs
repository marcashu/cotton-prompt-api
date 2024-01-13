using CottonPrompt.Infrastructure.Constants;
using CottonPrompt.Infrastructure.Entities;
using Microsoft.EntityFrameworkCore;

namespace CottonPrompt.Infrastructure.Services.Designs
{
    public class DesignService(CottonPromptContext dbContext) : IDesignService
    {
        public async Task PostCommentAsync(int id, string comment, Guid userId)
        {
            try
            {
                var design = await dbContext.OrderDesigns.Include(od => od.Order).SingleOrDefaultAsync(od => od.Id == id);

                if (design is null) return;

                // add comment
                var designComment = new OrderDesignComment
                {
                    OrderDesignId = id,
                    UserId = userId,
                    Comment = comment,
                    CreatedBy = userId
                };

                design.OrderDesignComments.Add(designComment);

                // update order status
                var order = design.Order;

                if (order.CheckerStatus != OrderStatuses.RequestedReupload)
                {
                    order.CheckerStatus = OrderStatuses.RequestedReupload;
                    order.UpdatedBy = userId;
                    order.UpdatedOn = DateTime.UtcNow;
                }

                if (order.ArtistStatus != OrderStatuses.ForReupload)
                {
                    order.ArtistStatus = OrderStatuses.ForReupload;
                    order.UpdatedBy = userId;
                    order.UpdatedOn = DateTime.UtcNow;
                }
                
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
