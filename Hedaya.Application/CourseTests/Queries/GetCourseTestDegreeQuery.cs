using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.CourseTests.Queries
{
    public class GetCourseTestDegreeQuery : IRequest<double>
    {
        public string UserId { get; set; }
        public int CourseTestId { get; set; }
    }

    public class GetCourseTestDegreeQueryHandler : IRequestHandler<GetCourseTestDegreeQuery, double>
    {
        private readonly IApplicationDbContext _context;

        public GetCourseTestDegreeQueryHandler(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<double> Handle(GetCourseTestDegreeQuery request, CancellationToken cancellationToken)
        {
            var traineeId = await _context.Trainees
                .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                .Select(a => a.Id)
                .FirstOrDefaultAsync(cancellationToken);

            var courseTest = await _context.CourseTests.FindAsync(request.CourseTestId);
            if (courseTest == null) throw new ArgumentException("Invalid Course Test Id");

            if (courseTest.Status != CourseTestStatus.Closed) throw new ArgumentException("Course Test is not closed yet");

            var totalQuestions = await _context.Questions
                .CountAsync(q => q.CourseTestId == request.CourseTestId, cancellationToken);

            var correctAnswersCount = await _context.TraineeAnswers
                .Where(ta => ta.TraineeId == traineeId && ta.CourseTestId == request.CourseTestId && ta.Score == 1)
                .CountAsync(cancellationToken);

            var TraineeDegree = Math.Round((double)correctAnswersCount / totalQuestions * 100, 2);
            return TraineeDegree;
        }
    }

}
