using Hedaya.Application.Courses.Models;
using Hedaya.Application.Favourites.Commands;
using Hedaya.Application.Favourites.Queries;
using Hedaya.Application.MethodologicalExplanations.Models;
using Hedaya.Application.Podcasts.Models;
using Hedaya.Application.TrainingPrograms.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class FavouritesController : BaseController<FavouritesController>
    {
        [HttpGet("GetFavouriteCourses")]
        public async Task<ActionResult<List<CourseDto>>> GetFavouriteCourses(int PageNumber = 1)
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
        public async Task<ActionResult<List<TrainingProgramDto>>> GetFavouritePrograms(int PageNumber = 1)
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


        [HttpGet("GetFavouriteExplanations")]
        public async Task<ActionResult<List<MethodlogicalExplanationDto>>> GetFavouriteExplanations(int PageNumber = 1)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var query = new GetFavoriteExplanationsQuery { UserId = userId, PageNumber = PageNumber };
            var explanations = await Mediator.Send(query);

            return Ok(explanations);
        }

        [HttpGet("GetFavouritePodcastsQuery")]
        public async Task<ActionResult<List<PodcastDTO>>> GetFavouritePodcastsQuery(int pageNumber = 1)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var query = new GetFavoritePodcastsQuery { UserId = userId, PageNumber = pageNumber };
            var podcasts = await Mediator.Send(query);

            return Ok(podcasts);
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

           
            return Ok(await Mediator.Send(command));

        }


        [HttpPut("ChangeStatusCourseFavourite")]
        public async Task<IActionResult> ToggleCourseFavorite(int id, bool isFavorite)
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


            return Ok(await Mediator.Send(command));
        }


        [HttpPut("ChangeStatusMethodologicalExplanationFavourite")]
        public async Task<IActionResult> ToggleMethodologicalExplanationFavorite(int id, bool isFavorite)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var command = new ToggleMethodologicalExplanationFavouriteCommand
            {
                Id = id,
                UserId = userId,
                IsFavourite = isFavorite
            };

          

            return Ok(await Mediator.Send(command));
        }
        [HttpPut("ChangePodcastFavouriteStatus")]
        public async Task<IActionResult> TogglePodcastFavourite(int id, bool isFavorite)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var command = new TogglePodcastFavouriteCommand
            {
                Id = id,
                UserId = userId,
                IsFavourite = isFavorite
            };

            return Ok(await Mediator.Send(command));
        }






    }
}
