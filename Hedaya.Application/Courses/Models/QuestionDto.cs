using Hedaya.Domain.Enums;

namespace Hedaya.Application.Courses.Models
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public QuestionType Type { get; set; }
        public List<AnswerDto> Answers { get; set; }
    }
}
