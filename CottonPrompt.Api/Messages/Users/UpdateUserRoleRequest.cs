using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Users
{
    public class UpdateUserRoleRequest
    {
        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();

        [Required]
        public Guid UpdatedBy { get; set; }
    }
}
