using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Orders
{
    public class UpdateUserGroupRequest
    {
        [Required]
        public string Name { get; set; }

        public IEnumerable<Guid> UserIds { get; set; } = Enumerable.Empty<Guid>();

        [Required]
        public Guid UpdatedBy { get; set; }
    }
}
