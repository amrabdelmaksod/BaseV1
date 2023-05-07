using Hedaya.Application.Courses.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Application.MethodologicalExplanations.Models;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Courses.Queries
{
    public class FilterCoursesQuery : IRequest<object>
    {
        public int PageNumber { get; set; }
        public int? CategoryId { get; set; }
        public bool? SortByDurationAscending { get; set; }
        public string? searchKeyword { get; set; }
        public string UserId { get; set; }

        public class FilterCoursesQueryHandler : IRequestHandler<FilterCoursesQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public FilterCoursesQueryHandler(IApplicationDbContext dbContext)
            {
                _context = dbContext;
            }

            public async Task<object> Handle(FilterCoursesQuery request, CancellationToken cancellationToken)
            {
                var traineeId = await _context.Trainees
                    .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                    .Select(a => a.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                // Filter by category ID if provided
                IQueryable<Course> query = _context.Courses;
                if (request.CategoryId.HasValue)
                {
                    query = query.Where(x => x.SubCategoryId == request.CategoryId);
                }

                // Apply search filter
                if (!string.IsNullOrEmpty(request.searchKeyword))
                {
                    query = query.Where(x => x.TitleEn.Contains(request.searchKeyword) || x.TitleAr.Contains(request.searchKeyword) || x.Description.Contains(request.searchKeyword));
                }

                // Sort by duration
                if (request.SortByDurationAscending.HasValue == true)
                {
                    query = query.OrderBy(x => x.Duration);
                }
                else
                {
                    query = query.OrderByDescending(x => x.Duration);
                }

                // Get total count for pagination
                int totalCount = await _context.Courses.CountAsync(cancellationToken);

                // Get paginated results
                int skip = (request.PageNumber - 1) * 10;
                var courses = await query.Skip(skip)
                    .Take(10)
                    .Select(c => new CourseDto
                    {
                        Id = c.Id,
                        Title = c.TitleAr,
                        StartDate = c.StartDate.ToString("d"),
                        Duration = c.Duration,
                        ImageUrl = c.ImageUrl,
                        IsFav = _context.TraineeCourseFavorites.Any(f => f.CourseId == c.Id && f.TraineeId == traineeId),
                        InstructorName = c.Instructor.GetFullName(),
                        InstructorImageUrl = c.Instructor.ImageUrl,
                        Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.SubCategory.NameAr : c.SubCategory.NameEn,
                        TrainingProgramId = c.TrainingProgramId,
                        CategoryId = c.SubCategoryId,
                    })
                    .ToListAsync(cancellationToken);

                // Get categories for dropdown
                var categories = await _context.SubCategories
                    .Select(x => new SubCategoryDto
                    {
                        Id = x.Id,
                        Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? x.NameAr : x.NameEn,
                        IconUrl = x.ImgIconUrl
                    })
                    .ToListAsync(cancellationToken);

                var response = new
                {
                    TotalCount = totalCount,
                    Categories = categories,
                    AllCourses = courses
                };

                return new { Result = response };
            }
        }
    }

}
