namespace Hedaya.Application.TeachingStaff.Models
{
    public class TeachingStaffDto
    {
        public string Id { get; set; }
        public string ImageURL { get; set; }
        public string FullName { get; set; }
        public string Description { get; set; }
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string Youtube { get; set; }
        public List<TutorialDto> Tutorials { get; set; }
    }

}
