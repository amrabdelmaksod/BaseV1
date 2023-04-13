using Hedaya.Application.Complexes.Queries;
using Hedaya.Application.Complexes.Queries.GetComplex;
using Hedaya.Application.SuggestionsAndComplaints.Commands.Create;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ComplexController : BaseController<ComplexController>
    {
        [HttpGet("vision-and-mission")]
        public async Task<IActionResult> GetVisionAndMission()
        {
            var query = new GetVisionAndMissionQuery();
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("cookies-and-log-files")]
        public async Task<IActionResult> GetCookiesAndLogFiles()
        {
            var query = new GetCookiesAndLogFilesQuery();
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("complex-data")]
        public async Task<IActionResult> GetComplexData()
        {
            var query = new GetComplexQuery();
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpPost("AddSuggestionsAndComplaints")]
        public async Task<ActionResult<int>> CreateSuggestionOrComplaint([FromBody] CreateSuggestionAndComplaintCommand command)
        {
            var validator = new CreateSuggestionAndComplaintCommandValidator();
            var validationResult = await validator.ValidateAsync(command);

            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var result = await Mediator.Send(command);

            return Ok(result);
        }

    }
}
