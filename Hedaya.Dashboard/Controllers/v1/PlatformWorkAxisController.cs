using Hedaya.Application.AboutPlatform.Commands.PlatformWorkAxes;
using Hedaya.Application.AboutPlatform.Models;
using Hedaya.Application.AboutPlatform.Queries.PlatformWorkAxis;
using Hedaya.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.Dashboard.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlatformWorkAxisController : BaseController<PlatformWorkAxisController>
    {


        [HttpGet("GetAll")]
        public async Task<ActionResult<PaginatedList<PlatformWorkAxesDto>>> GetPlatformWorkAxes([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllPlatformWorkAxesQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            };

            return await Mediator.Send(query);
        }

        [HttpGet("GetById")]
        public async Task<ActionResult<PlatformWorkAxesDto>> GetById(int id)
        {
            var query = new GetPlatformWorkAxesByIdQuery { Id = id };
            var result = await Mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }


        [HttpPost("CreatePlatformWorkAxis")]
        public async Task<ActionResult<int>> CreatePlatformWorkAxis(CreatePlatformWorkAxesCommand command)
        {
            try
            {
                var platformWorkAxisId = await Mediator.Send(command);
                return Ok(platformWorkAxisId);
            }
            catch (Exception ex)
            {
                // handle exceptions
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, UpdatePlatformWorkAxesCommand command)
        {
         
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePlatformWorkAxesCommand { Id = id });

            return NoContent();
        }


    }
}
