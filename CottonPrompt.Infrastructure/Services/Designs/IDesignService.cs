using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CottonPrompt.Infrastructure.Services.Designs
{
    public interface IDesignService
    {
        Task PostCommentAsync(int id, string comment, Guid userId);
    }
}
