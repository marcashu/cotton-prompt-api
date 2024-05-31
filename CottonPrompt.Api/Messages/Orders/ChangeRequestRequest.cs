using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class ChangeRequestRequest
    {
        [Required]
        public int DesignId { get; set; }

        [Required]
        public string Comment { get; set; }

        public IEnumerable<ImageReferenceRequest> ImageReferences { get; set; } = Enumerable.Empty<ImageReferenceRequest>();
    }
}
