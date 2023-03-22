using Hedaya.Application.Courses.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Courses.Queries
{
    public class GetCoursesQuery : IRequest<object>
    {
        public int PageSize { get; set; }
        public int PageNumber { get; set; }

        public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, object>
        {
            private readonly IApplicationDbContext _dbContext;

            public GetCoursesQueryHandler(IApplicationDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<object> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
            {
                var popularCourses = await _dbContext.Courses
                    .OrderByDescending(c => c.StartDate)
                    .Take(5)
                    .Select(c => new CourseDto
                    {
                        Id = c.Id,
                        Title = c.TitleAr,
                        StartDate = c.StartDate.ToShortDateString(),
                        Duration = c.Duration.ToString(),
                        ImageUrl = c.ImageUrl,
                        IsFav = false, // Set this based on user preferences
                        InstructorName =  c.Instructor.GetFullName(),
                        InstructorImageUrl = c.Instructor.ImageUrl,
                        Category =CultureInfo.CurrentCulture.TwoLetterISOLanguageName =="ar" ?  c.SubCategory.NameAr : c.SubCategory.NameEn,
                    })
                    .ToListAsync(cancellationToken);

                var allCourses = await _dbContext.Courses
                    .OrderByDescending(c => c.StartDate)
                    .Skip(request.PageSize * (request.PageNumber - 1))
                    .Take(request.PageSize)
                    .Select(c => new CourseDto
                    {
                        Id = c.Id,
                        Title = c.TitleAr,
                        StartDate = c.StartDate.ToShortDateString(),
                        Duration = c.Duration.ToString(),
                        ImageUrl = c.ImageUrl,
                        IsFav = false, // Set this based on user preferences
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

                return new { result = CoursesResult } ;
            }
        }

    }

}
