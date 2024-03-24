using CottonPrompt.Api.Messages.Rates;
using CottonPrompt.Infrastructure.Models.Rates;
using CottonPrompt.Infrastructure.Services.Rates;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RatesController(IRatesService rateService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<RatesModel>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await rateService.GetAsync();
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateRatesRequest request)
        {
            await rateService.UpdateAsync(request.QualityControlRate, request.ChangeRequestRate, request.UpdatedBy);
            return NoContent();
        }
    }
}
