using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class ResolveRequest
    {
        [Required]
        public Guid ResolvedBy { get; set; }
    }
}
