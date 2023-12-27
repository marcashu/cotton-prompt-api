using CottonPrompt.Api.Messages.Designs;
using CottonPrompt.Infrastructure.Services.Designs;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DesignsController(IDesignService designService) : ControllerBase
    {
        [HttpPost("{id}/comments")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> PostCommentAsync([FromRoute] int id, [FromBody] PostCommentRequest request)
        {
            await designService.PostCommentAsync(id, request.Comment, request.UserId);
            return NoContent();
        }
    }
}
