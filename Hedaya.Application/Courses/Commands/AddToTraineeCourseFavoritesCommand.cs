using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;

using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Courses.Commands
{


    namespace Hedaya.Application.TraineeCourseFavorites.Commands
    {
        public class AddToTraineeCourseFavoritesCommand : IRequest
        {
            public int CourseId { get; set; }
            public string userId { get; set; }
        }

        public class AddToTraineeCourseFavoritesCommandHandler : IRequestHandler<AddToTraineeCourseFavoritesCommand>
        {
            private readonly IApplicationDbContext _context;

            public AddToTraineeCourseFavoritesCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(AddToTraineeCourseFavoritesCommand request, CancellationToken cancellationToken)
            {
                var traineeId = await _context.Trainees
                     .Where(a => a.AppUserId == request.userId && !a.Deleted)
                     .Select(a => a.Id)
                     .FirstOrDefaultAsync(cancellationToken);
                
                var entity = new TraineeCourseFavorite
                {
                    CourseId = request.CourseId,
                    TraineeId = traineeId
                };

                _context.TraineeCourseFavorites.Add(entity);

                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }
    }

}
