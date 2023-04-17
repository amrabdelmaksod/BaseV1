using Hedaya.Application.Blogs.Queries;
using Hedaya.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
  
    public class BlogsController : BaseController<BlogsController>
    {
        private readonly ILoggerManager _logger;
        public BlogsController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet("GetAllBlogs")]
        public async Task<IActionResult> GetAllBlogs(int PageNumber = 1)
        {
            return Ok(await Mediator.Send(new GetAllBlogsQuery { PageNumber = PageNumber}));
        }


        [HttpGet("details")]
        public async Task<IActionResult> GetBlogById(int id)
        {
            return Ok(await Mediator.Send(new GetBlogById { Id = id }));
        }
    }
}
