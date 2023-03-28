using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Courses.Commands
{
    public class MarkLessonCompletedCommand : IRequest<Unit>
    {
        public string UserId { get; set; }
        public int LessonId { get; set; }

        public class MarkLessonCompletedCommandHandler : IRequestHandler<MarkLessonCompletedCommand, Unit>
        {
            private readonly IApplicationDbContext _context;

            public MarkLessonCompletedCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(MarkLessonCompletedCommand request, CancellationToken cancellationToken)
            {
                
                    var traineeId = await _context.Trainees
                    .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                    .Select(a => a.Id)
                    .FirstOrDefaultAsync(cancellationToken);



                    var Lesson = await _context.Lessons.FirstOrDefaultAsync(a => a.Id == request.LessonId);
                    var TraineeLesson =
                        new TraineeLesson
                        {
                            LessonId = request.LessonId,
                            TraineeId = traineeId,
                            IsCompleted = true
                        };


                    await _context.TraineeLessons.AddAsync(TraineeLesson);

                    await _context.SaveChangesAsync(cancellationToken);


                    return Unit.Value;
              
                
            }
        }

    }

}
