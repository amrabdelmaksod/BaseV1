using Hedaya.Application.Courses.Commands;
using Hedaya.Application.Courses.Commands.Hedaya.Application.TraineeCourseFavorites.Commands;
using Hedaya.Application.Courses.Models;
using Hedaya.Application.Courses.Queries;
using Hedaya.Application.CourseTests.Commands;
using Hedaya.Application.CourseTests.Queries;
using Hedaya.Application.Enrollments.Commands;
using Hedaya.Application.Enrollments.Models;
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
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var query = new GetCoursesQuery
            {
                PageNumber = pageNumber,
                UserId = userId
            };

            var result = await Mediator.Send(query);

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
        public async Task<IActionResult> FilterCourses([FromQuery] FilterCourseDto model)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var query = new FilterCoursesQuery 
            { 
                CategoryId = model.CategoryId ,
                PageNumber = model.PageNumber,
                searchKeyword = model.searchKeyword,
                SortByDurationAscending = model.SortByDurationAscending,
                UserId = userId,
        };
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("Details")]
        public async Task<ActionResult<object>> GetCourseDetails(int courseId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var query = new GetCourseDetailsQuery {CourseId = courseId ,UserId = userId};
            var result = await Mediator.Send(query);
            if(result is null)
                if (result == null)
                    return BadRequest(new { error = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"{courseId}عفوا لا يوجد دورة بهذا المعرف" : $"Sorry The Course With this id : {courseId} is not found" });
            return Ok(result);
        }


        [HttpGet("GetForumByCourseId")]
        public async Task<ActionResult<object>> GetForumByCourseId(int courseId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var query = new GetForumByCourseIdQuery
            {
                CourseId = courseId,
                UserId = userId
            };

            var result = await Mediator.Send(query);

            return Ok(result);
        }



        [HttpPost("AddCourseToFavourite")]
        public async Task<IActionResult> AddCourseToFavourite(int courseId)
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            await Mediator.Send(new AddToTraineeCourseFavoritesCommand { CourseId = courseId, userId = userId });

            return Ok();
        }

        [HttpPost("MarkLessonCompleted")]
        public async Task<IActionResult> MarkLessonCompleted(int lessonId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var command = new MarkLessonCompletedCommand { UserId = userId, LessonId = lessonId };
            await Mediator.Send(command);
            return Ok();
        }

        [HttpPost("submit-test")]
        public async Task<IActionResult> SubmitTest(int CourseTestId, Dictionary<int, string[]> Answers)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }


            var command = new SubmitTestCommand { CourseTestId = CourseTestId,  Answers = Answers , UserId =userId };
            var result = await Mediator.Send(command);

            if (result)
            {
                return Ok();
            }
            else
            {
                return BadRequest();
            }
        }


        [HttpGet("GetCourseTestDegree")]
        public async Task<IActionResult> GetCourseTestDegree(int courseTestId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var query = new GetCourseTestDegreeQuery
            {
                UserId = userId,
                CourseTestId = courseTestId
            };

            var degree = await Mediator.Send(query);

            return Ok(degree);
        }





        [HttpGet("GetMyCourses")]
        public async Task<ActionResult<object>> GetMyCourses(int PageNumber, int Type)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var query = new GetMyCoursesQuery
            {
                PageNumber = PageNumber,
                UserId = userId,
                TypeId = Type
            };

            var result = await Mediator.Send(query);

            return Ok(result);
        }



        [HttpPost("EnrollInCourse")]
        public async Task<ActionResult> EnrollInCourse(int CourseId)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var command = new EnrollInCourseCommand
            {
               
                UserId = userId,
                CourseId = CourseId,
            };
          

            return Ok(await Mediator.Send(command));
        }



    }

}
