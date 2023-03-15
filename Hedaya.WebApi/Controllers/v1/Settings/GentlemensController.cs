using Hedaya.Application.GentlemenScholars.DTOs;
using Hedaya.Application.GentlemenScholars.Queries;
using Hedaya.Application.Infrastructure;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class GentlemensController : BaseController<GentlemensController>
    {
        private readonly ILogger<GentlemensController> _logger;
        public GentlemensController(ILogger<GentlemensController> logger)
        {

            _logger = logger;

        }
    

        [HttpGet("GetAllGentlemenPaginated")]
        public async Task<ActionResult<PagedResults<GentlemenScholarDto>>> GetAllGentlemenPaginated([FromQuery] GetAllGentlemenSchoolarsPaginatedQuery request)
        {
            var result = await Mediator.Send(request);

            return Ok(result);
        }


        [HttpGet("GetGentlemenById")]
        public async Task<IActionResult> GetGentlemenById(string id)
        {
            var result = await Mediator.Send(new GetGentlemenScholarByIdQuery { Id = id});

            return Ok(result);
        }

    }
}
