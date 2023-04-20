using Hedaya.Application.Enrollments.Commands;
using Hedaya.Application.Enrollments.Models;
using Hedaya.Application.TrainingPrograms.Models;
using Hedaya.Application.TrainingPrograms.Queries;
using Microsoft.AspNetCore.Mvc;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
        public async Task<ActionResult<List<TrainingProgramDto>>> GetAllTrainingPrograms(int PageNumber = 1)
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

        [HttpGet("GetProgramById")]
        public async Task<ActionResult<object>> GetProgramById(int programId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var query = new GetProgramByIdQuery { ProgramId = programId, UserId = userId };
            var result = await Mediator.Send(query);
            return result;
        }


        [HttpPost("Enroll")]
        public async Task<ActionResult> EnrollInProgram([FromBody] EnrollInProgramDto dto)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var command = new EnrollInProgramCommand 
            { 
                Email = dto.Email ,
                FullName = dto.FullName ,
                MobileNumber = dto.MobileNumber ,
                ProgramId = dto.ProgramId ,
                UserId = userId
            };
            await Mediator.Send(command);

            return Ok();
        }


    }
}
