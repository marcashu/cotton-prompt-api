using CottonPrompt.Infrastructure.Models.Artists;
using CottonPrompt.Infrastructure.Services.Artists;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController(IArtistService artistService) : ControllerBase
    {
        [HttpGet("{id}/can-claim")]
        [ProducesResponseType<IEnumerable<CanArtistClaimModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CanClaimAsnyc([FromRoute] Guid id)
        {
            var result = await artistService.CanClaimAsync(id);
            return Ok(result);
        }
    }
}
    