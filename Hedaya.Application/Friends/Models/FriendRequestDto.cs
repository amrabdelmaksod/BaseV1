namespace Hedaya.Application.Friends.Models
{
    public class FriendRequestDto
    {
        public string Id { get; set; }
        public string TraineeId { get; set; }
        public string FriendId { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime? AcceptedDate { get; set; }
        public string FriendName { get; set; }
    }
}
