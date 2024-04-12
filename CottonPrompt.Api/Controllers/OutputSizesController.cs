using CottonPrompt.Api.Messages.OutputSizes;
using CottonPrompt.Infrastructure.Models.OutputSizes;
using CottonPrompt.Infrastructure.Services.OutputSizes;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OutputSizesController(IOutputSizeService designBracketService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<IEnumerable<OutputSize>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetOutputSizesRequest request)
        {
            var result = await designBracketService.GetAsync(request.HasActiveFilter, request.Active);
            return Ok(result);
        }

        [HttpPost("swap")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> SwapAsync([FromBody] SwapOutputSizesRequest request)
        {
            await designBracketService.SwapAsync(request.Id1, request.Id2, request.UserId);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateOutputSizeRequest request)
        {
            await designBracketService.UpdateAsync(id, request.Value, request.UserId);
            return NoContent();
        }

        [HttpGet("{id}/orders/count")]
        [ProducesResponseType<GetOutputSizeOrdersCountModel>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrdersCountAsync([FromRoute] int id)
        {
            var result = await designBracketService.GetOrdersCountAsync(id);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await designBracketService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/enable")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> EnableAsync([FromRoute] int id, [FromBody] EnableOutputSizeRequest request)
        {
            await designBracketService.EnableAsync(id, request.UserId);
            return NoContent();
        }

        [HttpPost("{id}/disable")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DisableAsync([FromRoute] int id, [FromBody] DisableOutputSizeRequest request)
        {
            await designBracketService.DisableAsync(id, request.UserId);
            return NoContent();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOutputSizeRequest request)
        {
            await designBracketService.CreateAsync(request.Value, request.UserId);
            return NoContent();
        }
    }
}
    