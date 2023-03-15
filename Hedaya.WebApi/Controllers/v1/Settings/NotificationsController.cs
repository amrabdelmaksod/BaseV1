using Hedaya.Application.Notifications.Queries;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NotificationsController : BaseController<NotificationsController>
    {
        [HttpGet]
        public async Task<IActionResult> GetNotifications()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            var query = new GetNotificationsByUserIdQuery(userId);
            var notifications = await Mediator.Send(query);
            return Ok(notifications);
        }

     
    }
}
