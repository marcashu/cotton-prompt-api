using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.DesignBrackets
{
    public class CreateDesignBracketRequest
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
