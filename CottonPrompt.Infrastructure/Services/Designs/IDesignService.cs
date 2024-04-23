using CottonPrompt.Infrastructure.Models;

namespace CottonPrompt.Infrastructure.Services.Designs
{
    public interface IDesignService
    {
        Task PostCommentAsync(int id, string comment, Guid userId);

        Task<DownloadModel> DownloadAsync(int id);
    }
}
