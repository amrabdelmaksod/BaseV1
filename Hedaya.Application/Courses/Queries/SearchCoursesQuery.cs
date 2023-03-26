using Hedaya.Application.Courses.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Courses.Queries
{
    public class SearchCoursesQuery : IRequest<object>
    {
        public string SearchQuery { get; set; }
        public int PageNumber { get; set; }

        public class SearchCoursesQueryHandler : IRequestHandler<SearchCoursesQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public SearchCoursesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(SearchCoursesQuery request, CancellationToken cancellationToken)
            {
                var PageSize = 10;

             

                var allCourses = _context.Courses
                    .OrderByDescending(c => c.StartDate)
                    .Select(c => new CourseDto
                    {
                        Id = c.Id,
                        Title = c.TitleAr,
                        StartDate = c.StartDate.ToString("d"),
                        Duration = c.Duration,
                        ImageUrl = c.ImageUrl,
                        IsFav = false, // ToDo User Favourites
                        InstructorName = c.Instructor.GetFullName(),
                        InstructorImageUrl = c.Instructor.ImageUrl,
                        Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.SubCategory.NameAr : c.SubCategory.NameEn,
                    });

                if (!string.IsNullOrEmpty(request.SearchQuery))
                {
                    allCourses = allCourses
                        .Where(c => c.Title.Contains(request.SearchQuery)
                                    || c.InstructorName.Contains(request.SearchQuery)
                                    || c.Category.Contains(request.SearchQuery));
                }

            
                var allCoursesPage = await allCourses
                    .Skip(PageSize * (request.PageNumber - 1))
                    .Take(PageSize)
                    .ToListAsync(cancellationToken);

              

                return new { result = allCoursesPage };
            }
        }

    }
}
