using CottonPrompt.Api.Messages.Orders;
using CottonPrompt.Infrastructure.Models.UserGroups;
using CottonPrompt.Infrastructure.Services.UserGroups;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace CottonPrompt.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserGroupsController(IUserGroupService userGroupService) : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType<GetUserGroupsModel>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAsync()
        {
            var result = await userGroupService.GetAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        [ProducesResponseType<GetUserGroupModel>((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            var result = await userGroupService.GetByIdAsync(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserGroupRequest request)
        {
            await userGroupService.CreateAsync(request.Name, request.UserIds, request.CreatedBy);
            return NoContent();
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id, [FromBody] UpdateUserGroupRequest request)
        {
            await userGroupService.UpdateAsync(id, request.Name, request.UserIds, request.UpdatedBy);
            return NoContent();
        }
    }
}
    