using Hedaya.Domain.Enums;

namespace Hedaya.Application.Notifications.Models
{
    public class NotificationLiDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }
        public DateTime Date { get; set; }
    }
}
