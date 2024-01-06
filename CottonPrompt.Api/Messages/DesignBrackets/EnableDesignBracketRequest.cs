using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.DesignBrackets
{
    public class EnableDesignBracketRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
