using Hedaya.Application.PrayerTimes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class PrayerTimesController : BaseController<PrayerTimesController>
    {
        [HttpGet("{latitude}/{longitude}")]
        public async Task<IActionResult> GetPrayerTimes(double latitude = 30.033333, double longitude = 31.233334)
        {
            try
            {             
                var result = await Mediator.Send(new GetPrayerTimesQuery(latitude, longitude));
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
