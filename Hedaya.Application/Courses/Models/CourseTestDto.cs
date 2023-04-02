namespace Hedaya.Application.Courses.Models
{
    public class CourseTestDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int QuestionsCount { get; set; }
        public List<QuestionDto> Questions { get; set; }
    }
}
