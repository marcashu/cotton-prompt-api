using CottonPrompt.Infrastructure.Services.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Authorize]
    [RequiredScope("access_as_user")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController(IUserService userService) : ControllerBase
    {
        [HttpGet("login")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> LoginAsync()
        {
            var user = User;

            var idClaim = user.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier");
            var nameClaim = user.FindFirst("name");
            var emailClaim = user.FindFirst("preferred_username");

            if (idClaim is null || nameClaim is null || emailClaim is null)
            {
                return BadRequest("Missing required user claims");
            }

            var id = Guid.Parse(idClaim.Value);
            var name = nameClaim.Value;
            var email = emailClaim.Value;
            await userService.LoginAsync(id, name, email);
            return NoContent();
        }
    }
}
    