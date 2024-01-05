using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.DesignBrackets
{
    public class UpdateRequest
    {
        [Required]
        public string Value { get; set; }
    }
}
