using Hedaya.Application.AboutPlatform.Commands.PlatformFeatures.Create;
using Hedaya.Application.AboutPlatform.Commands.PlatformFeatures.Delete;
using Hedaya.Application.AboutPlatform.Commands.PlatformFeatures.Update;
using Hedaya.Application.AboutPlatform.Models;
using Hedaya.Application.AboutPlatform.Queries.PlatformFeatures;
using Hedaya.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.Dashboard.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class PlatformFeaturesController : BaseController<PlatformFeaturesController>
    {

        [HttpGet("GetAll")]
        public async Task<ActionResult<PaginatedList<PlatformFeaturesDto>>> GetAll(int pageNumber = 1, int pageSize = 10)
        {
            var query = new GetAllPlatformFeaturesQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await Mediator.Send(query);
            return Ok(result);
        }
        [HttpGet("GetById")]
        public async Task<ActionResult<PlatformFeaturesDto>> GetById(int id)
        {
            var query = new GetPlatformFeatureByIdQuery { Id = id };
            var result = await Mediator.Send(query);

            return Ok(result);
        }


        [HttpPost("CreatePlatformFeature")]
        public async Task<IActionResult> CreatePlatformFeature(CreatePlatformFeatureCommand command)
        {
            
                var id = await Mediator.Send(command);

                return Ok(id);
           
        }

        [HttpPut("UpdatePlatformFeature")]
        public async Task<IActionResult> UpdatePlatformFeature(int id, [FromBody] UpdatePlatformFeatureDto dto)
        {
            
            var command = new UpdatePlatformFeatureCommand { Id = id ,Title = dto.Title,Description = dto.Description};
            await Mediator.Send(command);
            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var command = new DeletePlatformFeatureCommand { Id = id };
            await Mediator.Send(command, cancellationToken);

            return Ok();
        }


    }
}
