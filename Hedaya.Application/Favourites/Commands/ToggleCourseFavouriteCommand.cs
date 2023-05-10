using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Favourites.Commands
{
    public class ToggleCourseFavouriteCommand : IRequest<object>
    {
        public int Id { get; set; }
        public bool IsFavourite { get; set; }
        public string UserId { get; set; }

        public class ToggleCourseFavouriteCommandHandler : IRequestHandler<ToggleCourseFavouriteCommand,object>
        {
            private readonly IApplicationDbContext _context;

            public ToggleCourseFavouriteCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(ToggleCourseFavouriteCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var course = await _context.Courses.FindAsync(request.Id);
                    if (course == null)
                    {
                        throw new ArgumentException("Invalid course ID");
                    }

                    var traineeId = await _context.Trainees
                                       .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                                       .Select(a => a.Id)
                                       .FirstOrDefaultAsync(cancellationToken);

                    var trainee = await _context.Trainees.FindAsync(traineeId);
                    if (trainee == null)
                    {
                        throw new ArgumentException("Invalid trainee ID");
                    }
                    var favouriteCourse = await _context.TraineeCourseFavorites
                        .FirstOrDefaultAsync(f => f.TraineeId == traineeId && f.CourseId == request.Id);

                    if (request.IsFavourite && favouriteCourse == null)
                    {
                        _context.TraineeCourseFavorites.Add(new TraineeCourseFavorite
                        {
                            TraineeId = traineeId,
                            CourseId = request.Id
                        });
                    }
                    else if (!request.IsFavourite && favouriteCourse != null)
                    {
                        _context.TraineeCourseFavorites.Remove(favouriteCourse);
                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    return new { ItemId = course.Id };
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
