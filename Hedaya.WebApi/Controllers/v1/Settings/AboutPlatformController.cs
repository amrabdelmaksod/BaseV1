using Hedaya.Application.AboutPlatform.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    
    public class AboutPlatformController : BaseController<AboutPlatformController>
    {
        [HttpGet("AboutPlatform")]
        public async Task<IActionResult> GetAboutPlatform()
        {
            var result = await Mediator.Send(new GetAbouPlatformQuery());

            return Ok(result);
        }
    }
}
