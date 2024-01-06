using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.DesignBrackets
{
    public class DisableDesignBracketRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
