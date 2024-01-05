using CottonPrompt.Api.Messages.DesignBrackets;
using CottonPrompt.Infrastructure.Models.DesignBrackets;
using CottonPrompt.Infrastructure.Services.DesignBrackets;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignBracketsController(IDesignBracketService designBracketService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<IEnumerable<DesignBracket>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await designBracketService.GetAsync();
            return Ok(result);
        }

        [HttpPost("swap")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> SwapAsync([FromBody] SwapRequest request)
        {
            await designBracketService.SwapAsync(request.Id1, request.Id2);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateRequest request)
        {
            await designBracketService.UpdateAsync(id, request.Value);
            return NoContent();
        }
    }
}
    