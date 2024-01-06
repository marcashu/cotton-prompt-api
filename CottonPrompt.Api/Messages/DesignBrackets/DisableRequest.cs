using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.DesignBrackets
{
    public class DisableRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
