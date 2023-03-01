using Hedaya.Application.Users.Commands.UpdateRoles;
using Hedaya.Application.Users.Queries;
using Microsoft.AspNetCore.Mvc;


namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class UsersController : BaseController<UsersController>
    {
        [HttpGet("getAllUsers")]
        public async Task<IActionResult> GetAll()
        {
            var result = await Mediator.Send(new GetAllUsersQuery());
            return Ok(result);
        }

        [HttpGet("ManageRoles")]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var result = await Mediator.Send(new ManageRolesQuery{ userId = userId});
            return Ok(result);
        }

        [HttpPost("UpdateRoles")]

        public async Task<IActionResult> UpdateRoles(UpdateRolesCommand dto)
        {
            var result = await Mediator.Send(dto);
            return Ok(result);
        }

    }
}
