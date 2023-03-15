namespace Hedaya.Domain.Entities
{
    public class Tutorial
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string TeachingStaffId { get; set; }
        public virtual TeachingStaff TeachingStaff { get; set; }
    }
}
