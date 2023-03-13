using Hedaya.Application.PrayerTimes.Queries;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrayerTimesController : BaseController<PrayerTimesController>
    {
        [HttpGet("{latitude}/{longitude}")]
        public async Task<ActionResult<string>> GetPrayerTimes(double latitude, double longitude)
        {
            try
            {
             
                var result = await Mediator.Send(new GetPrayerTimesQuery(latitude, longitude));

                return JsonConvert.SerializeObject(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
