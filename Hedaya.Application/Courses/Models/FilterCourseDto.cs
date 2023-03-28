namespace Hedaya.Application.Courses.Models
{
    public class FilterCourseDto
    {
        public int PageNumber { get; set; }
        public List<int> CategoryIds { get; set; }
        public bool SortByDurationAscending { get; set; }
        public string? searchKeyword { get; set; }

    }
}
