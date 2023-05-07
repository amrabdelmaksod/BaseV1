using Hedaya.Application.Courses.Queries;
using Hedaya.Application.Helpers;
using Hedaya.Application.MethodologicalExplanations.Models;
using Hedaya.Application.MethodologicalExplanations.Queries;
using Hedaya.Application.TeachingStaff.Models;
using Hedaya.Application.TeachingStaff.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TeachingStaffController : BaseController<TeachingStaffController>
    {
        [HttpGet("teachingstaff")]
        public async Task<ActionResult<List<TeachingStaffDto>>> GetAllTeachingStaff(int PageNumber = 1)
        {
            var query = new GetAllTeachingStaffQuery { PageNumber = PageNumber };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetTeachingStaffById(string id)
        {
            var query = new GetTeachingStaffByIdQuery { Id = id };
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("GetAllMethodologicalExplanationsByInstructorId")]
        public async Task<ActionResult<PaginatedList<MethodlogicalExplanationDto>>> GetAllMethodologicalExplanationsByInstructorId(string instructorId, int PageNumber = 1)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var query = new GetAllMethodologicalExplanationsByInstructorIdQuery { InstructorId = instructorId, PageNumber = PageNumber,UserId = userId };
            var result = await Mediator.Send(query);

            return Ok(result);
        }


        [HttpGet("GetCoursesByInstructorId")]
        public async Task<IActionResult> GetCoursesByInstructorId(string instructorId, int PageNumber = 1)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var query = new GetCoursesByInstructorIdQuery { InstructorId = instructorId, UserId = userId ,PageNumber = PageNumber};
            var courses = await Mediator.Send(query);

            return Ok(courses);
        }



    }
}
