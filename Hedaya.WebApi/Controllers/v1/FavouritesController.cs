using System.Security.Claims;
using Hedaya.Application.Courses.Models;
using Hedaya.Application.Favourites.Commands;
using Hedaya.Application.Favourites.Queries;
using Hedaya.Application.TrainingPrograms.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FavouritesController : BaseController<FavouritesController>
    {
        [HttpGet("GetFavouriteCourses")]
        public async Task<ActionResult<List<CourseDto>>> GetFavouriteCourses(int PageNumber)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var query = new GetFavoriteCoursesQuery { UserId = userId,PageNumber = PageNumber };
            var courses = await Mediator.Send(query);

            return Ok(courses);
        }


        [HttpGet("GetFavouritePrograms")]
        public async Task<ActionResult<List<TrainingProgramDto>>> GetFavouritePrograms(int PageNumber)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }
            var query = new GetFavoriteTrainingProgramsQuery { UserId = userId ,PageNumber = PageNumber};
            var result = await Mediator.Send(query);

            return Ok(result);
        }


        [HttpPut("ChangeStatusProgramFavourite")]
        public async Task<IActionResult> ToggleFavoriteTrainingProgram(int id, bool IsFavourite)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var command = new ToggleFavoriteTrainingProgramCommand 
            {
                Id = id,
                UserId = userId,              
                IsFavourite = IsFavourite
            };

            await Mediator.Send(command);
            return Ok();

        }


        [HttpPut("ChangeStatusCourseFavourite")]
        public async Task<IActionResult> ToggleCourseFavorite(int id, [FromBody] bool isFavorite)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var command = new ToggleCourseFavouriteCommand
            {
                Id = id,
                UserId = userId,
                IsFavourite = isFavorite
            };

            await Mediator.Send(command);

            return Ok();
        }





    }
}
