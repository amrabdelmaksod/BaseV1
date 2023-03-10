using Hedaya.Application.CommonQuestions.Models;
using Hedaya.Application.CommonQuestions.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CommonQuestionsController : BaseController<CommonQuestionsController>
    {

        [HttpGet]
        public async Task<ActionResult<List<CommonQuestionDto>>> GetCommonQuestions()
        {


            return await Mediator.Send(new GetCommonQuestionsQuery()); ;
        }

    }
}
