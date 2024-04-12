using System.ComponentModel.DataAnnotations;

namespace CottonPrompt.Infrastructure.Constants
{
    internal enum UserRoles
    {
        [Display(Name ="Super Admin")]
        SuperAdmin,
        Admin,
        Checker,
        Artist,
    }
}
