using CottonPrompt.Infrastructure.Messages.Orders;
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
        [ProducesResponseType<IEnumerable<Order>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await orderService.GetAsync();
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType<ProblemDetails>((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateOrderRequest request)
        {
            await orderService.CreateAsync(request);
            return NoContent();
        }
    }
}
