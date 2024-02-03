using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Api.Messages.Users
{
    public class AddUserRequest
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();

        [Required]
        public Guid CreatedBy { get; set; }
    }
}
