using Hedaya.Application.TeachingStaff.Models;
using Hedaya.Application.TeachingStaff.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class TeachingStaffController : BaseController<TeachingStaffController>
    {
        [HttpGet("teachingstaff")]
        public async Task<ActionResult<List<TeachingStaffDto>>> GetAllTeachingStaff(int PageNumber = 1)
        {
            var query = new GetAllTeachingStaffQuery { PageNumber = PageNumber };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetTeachingStaffById(string id)
        {
            var query = new GetTeachingStaffByIdQuery { Id = id };
            var result = await Mediator.Send(query);

            return result;
        }


    }
}
