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
            var result = await orderService.GetAsync(request.Priority, request.ArtistId, request.HasArtistFilter);
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
    }
}
