using Hedaya.Application.Courses.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CoursesController : BaseController<CoursesController>
    {
     

        [HttpGet("GetAllCourses")]
        public async Task<ActionResult<object>> GetAllCourses([FromQuery] int pageNumber)
        {
            var query = new GetCoursesQuery
            {                
                PageNumber = pageNumber
            };

            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }

}
