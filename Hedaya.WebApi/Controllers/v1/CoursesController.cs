using Hedaya.Application.Courses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

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

        [HttpGet("SearchCourses")]
        public async Task<IActionResult> SearchCourses(int pageNumber = 1, string searchQuery = null)
        {
            var result = await Mediator.Send(new SearchCoursesQuery { PageNumber = pageNumber, SearchQuery = searchQuery });
            return Ok(result);
        }

        [HttpGet("courses/filter-options")]
        public async Task<ActionResult<object>> GetFilterOptions()
        {
            var query = new GetFilterOptionsQuery();
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("FilterCourses")]
        public async Task<IActionResult> FilterCourses([FromQuery] FilterCoursesQuery query)
        {
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("Details")]
        public async Task<ActionResult<object>> GetCourseDetails(int courseId)
        {
            var query = new GetCourseDetailsQuery {CourseId = courseId };
            var result = await Mediator.Send(query);
            if(result is null)
                if (result == null)
                    return BadRequest(new { error = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"{courseId}عفوا لا يوجد دورة بهذا المعرف" : $"Sorry The Course With this id : {courseId} is not found" });
            return Ok(result);
        }




    }

}
