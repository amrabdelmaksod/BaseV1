using Hedaya.Application.Blogs.Queries;
using Hedaya.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class BlogsController : BaseController<BlogsController>
    {
        private readonly ILoggerManager _logger;
        public BlogsController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet("getAllBlogs")]
        public async Task<IActionResult> GetAllBlogs()
        {
            return Ok(await Mediator.Send(new GetAllBlogsQuery()));
        }


        [HttpGet("details")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            return Ok(await Mediator.Send(new GetBlogById { Id = id}));
        }
    }
}
