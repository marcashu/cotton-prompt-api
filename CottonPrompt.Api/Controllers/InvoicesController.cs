using CottonPrompt.Api.Messages.DesignBrackets;
using CottonPrompt.Infrastructure.Models.Invoices;
using CottonPrompt.Infrastructure.Services.Invoices;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoicesController(IInvoiceService invoiceService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<IEnumerable<GetInvoicesModel>>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync([FromQuery] GetInvoicesRequest request)
        {
            var result = await invoiceService.GetAsync(request.UserId);
            return Ok(result);
        }
    }
}
    