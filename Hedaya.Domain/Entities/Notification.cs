using Hedaya.Domain.Entities.Authintication;
using Hedaya.Domain.Enums;

namespace Hedaya.Domain.Entities
{
    public class Notification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public NotificationType Type { get; set; }
        public DateTime Date { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }

    }

}
