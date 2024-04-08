using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.EmailTemplates
{
    public class UpdateOrderProofRequest
    {
        [Required]
        public string Content { get; set; }
    }
}
