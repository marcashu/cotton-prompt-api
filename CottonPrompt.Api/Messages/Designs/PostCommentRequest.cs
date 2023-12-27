using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Designs
{
    public class PostCommentRequest
    {
        [Required]
        public string Comment { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
