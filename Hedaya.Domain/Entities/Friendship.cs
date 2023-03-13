using Hedaya.Domain.Enums;

namespace Hedaya.Domain.Entities
{
    public class Friendship
    {
        public string TraineeId { get; set; }
        public Trainee Trainee { get; set; }
        public string FriendId { get; set; }
        public Trainee Friend { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public FriendshipStatus Status { get; set; }
    }

}
