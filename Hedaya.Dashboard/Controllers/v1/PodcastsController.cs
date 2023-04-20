using Hedaya.Application.Podcasts.Commands.Create;
using Hedaya.Application.Podcasts.Commands.Delete;
using Hedaya.Application.Podcasts.Commands.Update;
using Hedaya.Application.Podcasts.Models;
using Hedaya.Application.Podcasts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.Dashboard.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PodcastsController : BaseController<PodcastsController>
    {


        [HttpGet]
        public async Task<ActionResult<IEnumerable<PodcastDTO>>> GetAllPodcasts(int PageNumber = 1)
        {
            var query = new GetAllPodcastsQuery { PageNumber = PageNumber };
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("CreatePodcast")]
        public async Task<IActionResult> CreatePodcast([FromForm] CreatePodcastCommand command, CancellationToken cancellationToken)
        {
            await Mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPut("UpdatePodcast")]
        public async Task<IActionResult> UpdatePodcast(int id, [FromForm] UpdatePodcastCommand command)
        {
            command.Id = id;
            await Mediator.Send(command);

            return Ok();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeletePodcastCommand { Id = id });

            return NoContent();
        }



    }
}
