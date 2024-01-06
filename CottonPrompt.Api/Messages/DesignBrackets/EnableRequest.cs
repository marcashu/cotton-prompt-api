using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.DesignBrackets
{
    public class EnableRequest
    {
        [Required]
        public Guid UserId { get; set; }
    }
}
