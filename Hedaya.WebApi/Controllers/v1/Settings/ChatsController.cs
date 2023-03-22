using Hedaya.Application.Chats.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ChatsController : BaseController<ChatsController>
    {

        [HttpGet("GetChatQuestions")]
        public async Task<ActionResult<object>> GetChatQuestions()
        {

            return await Mediator.Send(new GetAllChatQuestionsQuery()); ;
        }
    }
}
