using Hedaya.Application.Courses.Models;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Courses.Queries
{
    public class GetCoursesQuery : IRequest<object>
    {
        public int PageNumber { get; set; }
       
        public string UserId { get; set; }

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

                var traineeId = await _context.Trainees
                     .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                     .Select(a => a.Id)
                     .FirstOrDefaultAsync(cancellationToken);

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
                        IsFav = _context.TraineeCourseFavorites.Any(f => f.CourseId == c.Id && f.TraineeId == traineeId),
                        InstructorName = c.Instructor.GetFullName(),
                        InstructorImageUrl = c.Instructor.ImageUrl,
                        Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.SubCategory.NameAr : c.SubCategory.NameEn,
                        TrainingProgramId = c.TrainingProgramId,
                        CategoryId = c.SubCategoryId,
                        VideoUrl = c.VideoUrl,
                        
                    })
                    .ToListAsync(cancellationToken);

                var allCoursesQuery = _context.Courses
                    .OrderByDescending(c => c.StartDate)
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
                        VideoUrl = c.VideoUrl,
                    });




                var allCourses = await allCoursesQuery
                    .Skip((request.PageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .ToListAsync(cancellationToken);


                var AllItems = new CourseLiDto
                {
                    AllCourses = allCourses,
                    PopularCourses = popularCourses,
                };

                var totalCount = await allCoursesQuery.CountAsync(cancellationToken);
                var totalPages = (int)Math.Ceiling((double)totalCount / PageSize);



                var paginatedList = new PaginatedList<CourseLiDto>(AllItems, totalCount, request.PageNumber, PageSize, totalPages);

           

          

                return new {Result = paginatedList } ;
            }
        }


    }
}