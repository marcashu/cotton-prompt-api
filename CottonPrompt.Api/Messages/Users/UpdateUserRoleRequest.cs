using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Users
{
    public class UpdateUserRoleRequest
    {
        [Required]
        public string Role { get; set; }

        [Required]
        public Guid UpdatedBy { get; set; }
    }
}
