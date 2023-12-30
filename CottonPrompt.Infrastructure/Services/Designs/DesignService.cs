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
                var orderDesign = await dbContext.OrderDesigns.Include(o => o.Order).SingleOrDefaultAsync(od => od.Id == id);

                if (orderDesign is null) return;

                orderDesign.Order.CheckerStatus = "In Review";
                orderDesign.Order.UpdatedBy = orderDesign.Order.CheckerClaimedBy;
                orderDesign.Order.UpdatedOn = DateTime.UtcNow;

                var designComment = new OrderDesignComment
                {
                    OrderDesignId = id,
                    Comment = comment,
                    CreatedBy = userId
                };

                orderDesign.OrderDesignComments.Add(designComment);

                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
