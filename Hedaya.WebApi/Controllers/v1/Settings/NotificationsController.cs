using Hedaya.Application.Notifications.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class NotificationsController : BaseController<NotificationsController>
    {
        [HttpGet]
        public async Task<IActionResult> GetNotifications(int pageNumber)
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            var query = new GetNotificationsByUserIdQuery(userId,pageNumber);
            var notifications = await Mediator.Send(query);
            return Ok(notifications);
        }

     
    }
}
