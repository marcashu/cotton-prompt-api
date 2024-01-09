using CottonPrompt.Api.Extensions;
using CottonPrompt.Api.Messages.Orders;
using CottonPrompt.Infrastructure.Models.Orders;
using CottonPrompt.Infrastructure.Services.Orders;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController(IOrderService orderService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<IEnumerable<GetOrdersModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetOrdersRequest request)
        {
            var result = await orderService.GetAsync(request.Priority, request.ArtistId, request.CheckerId, request.AvailableForArtists, request.AvailableForCheckers);
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<GetOrderModel>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await orderService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrderRequest request)
        {
            await orderService.CreateAsync(request.AsEntity());
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            await orderService.DeleteAsync(id);
            return NoContent();
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateOrderRequest request)
        {
            await orderService.UpdateAsync(request.AsEntity());
            return NoContent();
        }

        [HttpPost("{id}/artist")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AssignArtistAsync([FromRoute] int id, [FromBody] AssignArtristRequest request)
        {
            await orderService.AssignArtistAsync(id, request.ArtistId);
            return NoContent();
        }

        [HttpPost("{id}/checker")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> AssignCheckerAsync([FromRoute] int id, [FromBody] AssignCheckerRequest request)
        {
            await orderService.AssignCheckerAsync(id, request.CheckerId);
            return NoContent();
        }

        [HttpPost("{id}/designs")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> SubmitDesignAsync([FromRoute] int id, [FromBody] SubmitDesignRequest request)
        {
            var base64 = request.Design.Substring(request.Design.IndexOf("base64,") + 7);
            var bytes = Convert.FromBase64String(base64);
            var designStream = new MemoryStream(bytes);
            await orderService.SubmitDesignAsync(id, request.FileName, designStream);
            return NoContent();
        }

        [HttpPost("{id}/approve")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> ApproveAsync([FromRoute] int id)
        {
            await orderService.ApproveAsync(id);
            return NoContent();
        }

        [HttpPost("{id}/request-reupload")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> RequestReuploadAsync([FromRoute] int id)
        {
            await orderService.RequestReuploadAsync(id);
            return NoContent();
        }
    }
}
