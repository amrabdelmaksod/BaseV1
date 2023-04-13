using Hedaya.Application.AboutPlatform.Commands.PlatformFields;
using Hedaya.Application.AboutPlatform.Models;
using Hedaya.Application.AboutPlatform.Queries.PlatformFields;
using Hedaya.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.Dashboard.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PlatformFieldsController : BaseController<PlatformFieldsController>
    {
        private readonly IWebHostEnvironment _environment;

        public PlatformFieldsController(IWebHostEnvironment environment)
        {
            _environment = environment;
        }


        [HttpGet("GetAll")]
        public async Task<ActionResult<PaginatedList<PlatformFieldDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetPlatformFieldsQuery { PageNumber = pageNumber, PageSize = pageSize ,WebRootPath = _environment.WebRootPath};
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetPlatformFieldById")]
        public async Task<ActionResult<PlatformFieldDto>> GetPlatformFieldById(int id)
        {
              var query = new GetPlatformFieldByIdQuery { Id = id ,WebRootPath = _environment.WebRootPath};
                var platformField = await Mediator.Send(query);

                if (platformField == null)
                {
                    return NotFound();
                }

                return Ok(platformField);
           
        }


        [HttpPost("CreatePlatformField")]
        public async Task<ActionResult<int>> CreatePlatformField([FromForm]CreatePlatformFieldCommand command)
        {
            command.webrootpath = _environment.WebRootPath;
            var id = await Mediator.Send(command);

            if (id == 0)
            {
                return BadRequest();
            }

            return Ok(id);
        }

        [HttpPut("UpdatePlatformField")]
        public async Task<IActionResult> UpdatePlatformField(int id, [FromForm] UpdatePlatformFieldCommand command)
        {
            

            command.WebRootPath = _environment.WebRootPath;
            command.Id = id;
            
                await Mediator.Send(command);
           

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            
                var command = new DeletePlatformFieldCommand { Id = id };
            command.WebRootPath = _environment.WebRootPath;

            await Mediator.Send(command, cancellationToken);
                return Ok();
           
        }


    }
}
