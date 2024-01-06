using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.DesignBrackets
{
    public class UpdateDesignBracketRequest
    {
        [Required]
        public string Value { get; set; }

        [Required]
        public Guid UserId { get; set; }
    }
}
