using CottonPrompt.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonPrompt.Infrastructure.Services.Designs
{
    public class DesignService(CottonPromptContext dbContext) : IDesignService
    {
        public async Task PostCommentAsync(int id, string comment, Guid userId)
        {
            try
            {
                var designComment = new OrderDesignComment
                {
                    OrderDesignId = id,
                    Comment = comment,
                    CreatedBy = userId
                };

                dbContext.OrderDesignComments.Add(designComment);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
