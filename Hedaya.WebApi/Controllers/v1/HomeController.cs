using Hedaya.Application.Home.Queries;
using Hedaya.WebApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class HomeController : BaseController<HomeController>
    {
        private readonly ILoggerManager _loggerManager;
        public HomeController(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }


        [HttpGet]
        public async Task<IActionResult> GetHomeData()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            if (userId == null)
            {
                return Unauthorized();
            }

            var query = new GetHomeDataQuery { UserId = userId };
            var result = await Mediator.Send(query);
            return Ok(result);
        }


    }
}
