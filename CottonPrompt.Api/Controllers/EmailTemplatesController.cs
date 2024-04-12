using CottonPrompt.Api.Messages.EmailTemplates;
using CottonPrompt.Infrastructure.Models.Rates;
using CottonPrompt.Infrastructure.Services.EmailTemplates;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplatesController(IEmailTemplateService emailTemplateService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<EmailTemplatesModel>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await emailTemplateService.GetAsync();
            return Ok(result);
        }

        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateOrderProofAsync([FromBody] UpdateOrderProofRequest request)
        {
            await emailTemplateService.UpdateOrderReadyAsync(request.Content);
            return NoContent();
        }
    }
}
