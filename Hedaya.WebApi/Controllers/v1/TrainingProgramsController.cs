using Hedaya.Application.TrainingPrograms.Models;
using Hedaya.Application.TrainingPrograms.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TrainingProgramsController : BaseController<TrainingProgramsController>
    {
        public TrainingProgramsController()
        {
            
        }


        [HttpGet("GetAllTrainingPrograms")]
        public async Task<ActionResult<List<TrainingProgramDto>>> GetAllTrainingPrograms(int PageNumber)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var query = new GetAllTrainingProgramsQuery{ UserId = userId,PageNumber = PageNumber};
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet]
        public async Task<IActionResult> GetTrainingPrograms([FromQuery] FilterTrainingProgramsQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
