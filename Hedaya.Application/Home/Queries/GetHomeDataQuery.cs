using Hedaya.Application.Home.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Home.Queries
{
    public class GetHomeDataQuery : IRequest<object>
    {
        public string UserId { get; set; }

        public class GetHomeDataQueryHandler : IRequestHandler<GetHomeDataQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetHomeDataQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetHomeDataQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var traineeId = await _context.Trainees
                     .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                     .Select(a => a.Id)
                     .FirstOrDefaultAsync(cancellationToken);




                    var Offers = await _context.Offers.Where(a => !a.Deleted).Select(a => new OfferDto
                    {
                        Id = a.Id,
                        ImageUrl = a.ImageUrl,
                    }).ToListAsync(cancellationToken);

                    var blogs = await _context.Blogs.Where(a => !a.Deleted).Select(a => new BlogDto
                    {
                        Id = a.Id,
                        ImageUrl = a.ImagePath??"",
                        Description = a.Description??"",
                        Name = a.Title,
                        Date = a.CreationDate,
                    }).ToListAsync(cancellationToken);

                    var Categories = await _context.SubCategories.Where(a => !a.Deleted).Select(a => new CategoryDto
                    {
                        Id = a.Id,
                        ImageUrl = a.ImgIconUrl,
                        Name = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? a.NameAr : a.NameEn,

                    }).ToListAsync(cancellationToken);

                    var DiscoverCourses = await _context.Courses.Where(a => !a.Deleted).Select(a => new DiscoverCoursesDto
                    {
                        CategoryId = a.SubCategoryId,
                        CategoryName = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? a.SubCategory.NameAr : a.SubCategory.NameEn,
                        Id = a.Id,
                        Name = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? a.TitleAr : a.TitleEn,
                        Description = a.Description,
                        Duration = a.Duration,
                        InstructorId = a.InstructorId,
                        CourseImageUrl = a.ImageUrl,
                        InstructorImageUrl = a.Instructor.ImageUrl,
                        StartDate = a.StartDate.ToString("d"),
                        InstructorName = a.Instructor.GetFullName(),
                        IsFav = _context.TraineeCourseFavorites.Any(f => f.CourseId == a.Id && f.TraineeId == traineeId),

                    }).ToListAsync();

                    var MethodologicalExplanations = await _context.MethodologicalExplanations.Where(a => !a.Deleted).Select(a => new MethodologicalExplanationDto
                    {
                        Id = a.Id,
                        ImageUrl = a.ImageUrl,
                        Name = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? a.TitleAr : a.TitleEn,
                    }).ToListAsync();

                   
                    var MyCourses = await _context.Enrollments.Include(a => a.Course).ThenInclude(a => a.Instructor).Where(a => a.TraineeId == traineeId).Select(a => new MyCoursesDto
                    {
                        Id = a.CourseId,
                        Name = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? a.Course.TitleAr : a.Course.TitleEn,
                        InstructorName = a.Course.Instructor.GetFullName(),
                        CompleetedLessons = _context.TraineeLessons.Where(l => l.CourseId == a.CourseId && l.TraineeId == a.TraineeId&&l.IsCompleted).Count(),
                    }).ToListAsync();

                    foreach (var item in MyCourses)
                    {
                        item.CompleatedPercentage = await GetCourseCompletionRateAsync(item.Id, traineeId);
                    }

                    
                    var HomeResult = new HomeQueryDto
                    {
                        IntroVideoUrl = await _context.Complexes.Select(a => a.AboutPlatformVideoUrl).FirstOrDefaultAsync()??"",
                        Blogs = blogs,
                        Categories = Categories,
                        DiscoverCourses = DiscoverCourses,
                        MethodologicalExplanations = MethodologicalExplanations,
                        MyCourses = MyCourses,
                        Offers = Offers,
                    };






                    async Task<double> GetCourseCompletionRateAsync(int courseId, string traineeId)
                    {
                        var completedLessonsCount = await _context.TraineeLessons
                            .CountAsync(tl => tl.TraineeId == traineeId && tl.Lesson.CourseTopic.CourseId == courseId && tl.IsCompleted);

                        var totalLessonsCount = await _context.Lessons
                            .CountAsync(l => l.CourseTopic.CourseId == courseId);

                        if (totalLessonsCount == 0)
                        {
                            return 0;
                        }

                        return Math.Round(completedLessonsCount * 100.0 / totalLessonsCount, 2);
                    }



                    return new { Result = HomeResult };
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }

        }

    }

}
