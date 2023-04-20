using System.Globalization;
using Hedaya.Application.Courses.Models;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Favourites.Queries
{
    public class GetFavoriteCoursesQuery : IRequest<object>
    {
        public string UserId { get; set; }
        public int PageNumber { get; set; }
    }

    public class GetFavoriteCoursesQueryHandler : IRequestHandler<GetFavoriteCoursesQuery, object>
    {
        private readonly IApplicationDbContext _context;

        public GetFavoriteCoursesQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<object> Handle(GetFavoriteCoursesQuery request, CancellationToken cancellationToken)
        {
            var PageSize = 10;
            var traineeId = await _context.Trainees
                   .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                   .Select(a => a.Id)
                   .FirstOrDefaultAsync(cancellationToken);

            var trainee = await _context.Trainees.FindAsync(traineeId);
            if (trainee == null)
            {
                throw new ArgumentException("Invalid trainee ID");
            }

            var query = _context.Courses
                .Where(c => c.Favorites.Any(f => f.TraineeId == traineeId))
                .Include(c => c.Instructor)
                .Include(c => c.SubCategory);

            var totalCount = await query.CountAsync(cancellationToken);

            var totalPages = (int)Math.Ceiling((double)totalCount / PageSize);

            var courses = await query
                .Skip((request.PageNumber - 1) * PageSize)
                .Take(PageSize)
                .ToListAsync(cancellationToken);

            var courseDtos = courses.Select(c => new CourseDto
            {
                Id = c.Id,
                Title = c.TitleAr,
                StartDate = c.StartDate.ToString("yyyy-MM-dd"),
                Duration = c.Duration,
                ImageUrl = c.ImageUrl,
                IsFav = true,
                InstructorName = c.Instructor.GetFullName(),
                InstructorImageUrl = c.Instructor.ImageUrl,
                Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.SubCategory.NameAr : c.SubCategory.NameEn
            }).ToList();

            return new { Result = new PaginatedList<CourseDto>(courseDtos, totalCount, request.PageNumber, PageSize, totalPages) };
        }
    }
}
