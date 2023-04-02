using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using Hedaya.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.CourseTests.Commands
{
    public class SubmitTestCommand : IRequest<bool>
    {
        public string UserId { get; set; }
        public int CourseTestId { get; set; }
        public Dictionary<int, string[]> Answers { get; set; }


        public class SubmitTestCommandHandler : IRequestHandler<SubmitTestCommand, bool>
        {
            private readonly IApplicationDbContext _context;

            public SubmitTestCommandHandler(IApplicationDbContext dbContext)
            {
                _context = dbContext;
            }

            public async Task<bool> Handle(SubmitTestCommand request, CancellationToken cancellationToken)
            {
                var courseTest = new CourseTest
                {
                    Title = "Dummy Course Test",
                    Status = CourseTestStatus.Open,
                    CourseId = 1,
                    Questions = new List<Question>
    {
        new Question
        {
            Text = "What is the capital of France?",
            Answers = new List<Answer>
            {
                new Answer { Text = "Paris", IsCorrect = true },
                new Answer { Text = "London", IsCorrect = false },
                new Answer { Text = "Madrid", IsCorrect = false },
                new Answer { Text = "Berlin", IsCorrect = false }
            }
        },
        new Question
        {
            Text = "What is the largest planet in our solar system?",
            Answers = new List<Answer>
            {
                new Answer { Text = "Jupiter", IsCorrect = true },
                new Answer { Text = "Mars", IsCorrect = false },
                new Answer { Text = "Earth", IsCorrect = false },
                new Answer { Text = "Saturn", IsCorrect = false }
            }
        }
    }
                };

                _context.CourseTests.Add(courseTest);
                await _context.SaveChangesAsync();


                var traineeId = await _context.Trainees
                   .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                   .Select(a => a.Id)
                   .FirstOrDefaultAsync(cancellationToken);
                var trainee = await _context.Trainees.FindAsync(traineeId);
                if (trainee == null) throw new ArgumentException("Invalid Trainee Id");

                var courseTest = await _context.CourseTests.FindAsync(request.CourseTestId);
                if (courseTest == null) throw new ArgumentException("Invalid Course Test Id");

                if (courseTest.Status != CourseTestStatus.Open) throw new ArgumentException("Course Test is not open for submission");

                var questions = await _context.Questions
                    .Where(q => q.CourseTestId == request.CourseTestId)
                    .ToListAsync(cancellationToken);

                foreach (var question in questions)
                {
                    if (!request.Answers.TryGetValue(question.Id, out var selectedAnswers))
                    {
                        throw new ArgumentException($"Selected answers not found for question id {question.Id}");
                    }

                    var correctAnswers = await _context.Answers
                 .Where(a => a.QuestionId == question.Id && a.IsCorrect)
                 .Select(a => a.Text)
                 .ToArrayAsync(cancellationToken);


                    var traineeAnswer = new TraineeAnswer
                    {
                        Trainee = trainee,
                        CourseTest = courseTest,
                        Question = question,
                        SelectedAnswers = string.Join(",", selectedAnswers),
                        Score = correctAnswers.SequenceEqual(selectedAnswers) ? 1 : 0
                    };

                    _context.TraineeAnswers.Add(traineeAnswer);
                }

                courseTest.Status = CourseTestStatus.Closed;
                await _context.SaveChangesAsync(cancellationToken);

                return true;
            }
        }

    }

}
