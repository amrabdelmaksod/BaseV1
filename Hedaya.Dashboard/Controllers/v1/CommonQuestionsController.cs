using Hedaya.Application.CommonQuestions.Commands;
using Hedaya.Application.CommonQuestions.Models;
using Hedaya.Application.CommonQuestions.Queries;
using Hedaya.Application.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.Dashboard.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CommonQuestionsController : BaseController<CommonQuestionsController>
    {

        [HttpGet("GetAll")]
        public async Task<ActionResult<PaginatedList<CommonQuestionDto>>> GetCommonQuestions([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var query = new GetAllCommonQuestionsQuery { PageNumber = pageNumber, PageSize = pageSize };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("GetCommonQuestionById")]
        public async Task<ActionResult<CommonQuestionDto>> GetCommonQuestionById(int id)
        {
            var query = new GetCommonQuestionByIdQuery { Id = id };
            var result = await Mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return result;
        }



        [HttpPost("Create")]
        public async Task<ActionResult<int>> CreateCommonQuestion(CreateCommonQuestionCommand command)
        {
            var id = await Mediator.Send(command);
            return Ok(id);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateCommonQuestionCommand command)
        {
            
            command.Id = id;
            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("Delete")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            await Mediator.Send(new DeleteCommonQuestionCommand { Id = id }, cancellationToken);

            return NoContent();
        }

    }
}
