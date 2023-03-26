using Hedaya.Application.Courses.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Courses.Queries
{
    public class GetCoursesQuery : IRequest<object>
    {
    
        public int PageNumber { get; set; }

        public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetCoursesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
            {


              
                var PageSize = 10;

                var popularCourses = await _context.Courses
                    .OrderByDescending(c => c.StartDate)
                    .Take(5)
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
                    })
                    .ToListAsync(cancellationToken);

                var allCourses = await _context.Courses
                    .OrderByDescending(c => c.StartDate)
                    .Skip(PageSize * (request.PageNumber - 1))
                    .Take(PageSize)
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
                    })
                    .ToListAsync(cancellationToken);

                var CoursesResult = new CourseLiDto
                {
                    PopularCourses = popularCourses,
                    AllCourses = allCourses
                };

                return new { result = CoursesResult };
            }
        }

    }

}
