using Hedaya.Application.Users.Commands.ChangeStatus;
using Hedaya.Application.Users.Commands.CreateUser;
using Hedaya.Application.Users.Commands.DeleteUser;
using Hedaya.Application.Users.Commands.UpdateRoles;
using Hedaya.Application.Users.Commands.UpdateUser.Hedaya.Application.Users.Commands.EditUser;
using Hedaya.Application.Users.Commands.UpdateUser.Hedaya.Application.Users.Commands.UpdateUser;
using Hedaya.Application.Users.Models;
using Hedaya.Application.Users.Queries;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.Dashboard.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class UsersController : BaseController<UsersController>
    {
        private readonly ILogger<UsersController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UsersController(ILogger<UsersController> logger,UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        [HttpGet]
        public async Task<ActionResult<List<UsersLiDto>>> GetAllUsers(int pageNumber = 1)
        {
            var query = new GetAllUsersQuery { PageNumber = pageNumber };
            var result = await Mediator.Send(query);

            return Ok(result);
        }


        [HttpPost("create")]
        public async Task<IActionResult> CreateUser(CreateUserCommand command)
        {
            var validationResult = await new CreateUserCommandValidator(_userManager, _roleManager).ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var userId = await Mediator.Send(command);

            return Ok(userId);
        }

        [HttpPut("UpdateUser")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] EditUserDto userDto)
        {

            var command = new EditUserCommand 
            {  
                Id = id,
                Email = userDto.Email,
                FullName = userDto.FullName,
                DateOfBirth = userDto.DateOfBirth,
                Gender = userDto.Gender,
                Nationality = userDto.Nationality,
                Phone = userDto.Phone,
                RoleId = userDto.RoleId,
                UserType = userDto.UserType
            };
           



            var validationResult = await new EditUserCommandValidator(_userManager, _roleManager).ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await Mediator.Send(command);

            return Ok();
        }

        [HttpPut("ChangeUserStatus")]
        public async Task<IActionResult> ChangeUserStatus(string id, bool IsActive)
        {
        
            var command = new ChangeUserStatusCommand {UserId = id, IsActive = IsActive };
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("DeleteUser")]
        public async Task<IActionResult> DeleteUser(string id, string deleteReason)
        {
            var command = new DeleteUserCommand { Id = id, DeletedReason = deleteReason };
            var validationResult = await new DeleteUserCommandValidator(_userManager).ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            await Mediator.Send(command);

            return Ok();
        }


        [HttpGet("ManageRoles")]
        public async Task<IActionResult> ManageRoles(string userId)
        {
            var result = await Mediator.Send(new ManageRolesQuery { userId = userId });
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
