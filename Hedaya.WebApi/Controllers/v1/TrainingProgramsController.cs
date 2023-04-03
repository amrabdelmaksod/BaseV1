using Hedaya.Application.TrainingPrograms.Models;
using Hedaya.Application.TrainingPrograms.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainingProgramsController : BaseController<TrainingProgramsController>
    {
        public TrainingProgramsController()
        {
            
        }


        [HttpGet("GetAllTrainingPrograms")]
        public async Task<ActionResult<List<TrainingProgramDto>>> GetAllTrainingPrograms()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var query = new GetAllTrainingProgramsQuery{ UserId = userId};
            var result = await Mediator.Send(query);
            return Ok(result);
        }
    }
}
