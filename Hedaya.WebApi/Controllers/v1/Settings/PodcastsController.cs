﻿using Hedaya.Application.Podcasts.Models;
using Hedaya.Application.Podcasts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class PodcastsController : BaseController<PodcastsController>
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PodcastDTO>>> GetAllPodcasts(int pageNumber = 1)
        {
            var query = new GetAllPodcastsQuery { PageNumber = pageNumber};
            var result = await Mediator.Send(query);

            return Ok(result);
        }
    }
}
