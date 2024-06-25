using CottonPrompt.Infrastructure.Models;
using CottonPrompt.Infrastructure.Services.Checkers;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CheckersController(ICheckerService checkerService) : ControllerBase
    {
        [HttpGet("{id}/can-claim-training-group")]
        [ProducesResponseType<IEnumerable<CanDoModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> CanClaimTrainingGroupAsnyc([FromRoute] Guid id)
        {
            var result = await checkerService.CanClaimTrainingGroupAsync(id);
            return Ok(result);
        }
    }
}
    