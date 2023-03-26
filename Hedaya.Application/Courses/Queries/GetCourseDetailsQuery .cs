using Hedaya.Application.Courses.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;
using System.Globalization;
using System.Linq;

namespace Hedaya.Application.Courses.Queries
{
    // Query
    public class GetCourseDetailsQuery : IRequest<object>
    {
        public int CourseId { get; set; }
    }

    // Query Handler
    public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, object>
    {
        private readonly IApplicationDbContext _dbContext;

        public GetCourseDetailsQueryHandler(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<object> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
        {
            try
            {
              


                var course = await _dbContext.Courses
                        .Include(c => c.Instructor).Include(a=>a.SubCategory)
                        .FirstOrDefaultAsync(c => c.Id == request.CourseId, cancellationToken);

                if (course == null)
                {
                    return null;
                }

                var topics = await _dbContext.CourseTopics
                    .Include(ct => ct.Lessons)
                    .Where(ct => ct.CourseId == request.CourseId).OrderBy(a => a.SortIndex)
                    .Select(ct => new CourseTopicDto
                    {
                        Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? ct.NameAr : ct.NameEn,
                        Lessons = ct.Lessons.OrderBy(a => a.SortIndex).Select(l => new LessonDto
                        {
                            Id = l.Id,
                            Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? l.NameAr : l.NameEn,
                            Duration = l.Duration
                        }).ToList()
                    })

                    .ToListAsync(cancellationToken);

                var courseDetailsWithTopicsDto = new CourseDetailsDto
                {
                    Id = course.Id,
                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? course.TitleAr : course.TitleEn,
                    StartDate = course.StartDate.ToString("d"),
                    Duration = course.Duration,
                    ImageUrl = course.ImageUrl,
                    IsFav = false,
                    InstructorName = course.Instructor.GetFullName(),
                    InstructorImageUrl = course.Instructor.ImageUrl,
                    Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? course.SubCategory.NameAr : course.SubCategory.NameEn,
                    AboutCourse = course.AboutCourse,
                    Topics = topics,
                    CourseFeatures = course.CourseFeatures,
                    CourseSyllabus = course.CourseSyllabus,
                };

                return new { result = courseDetailsWithTopicsDto };
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }

          
        }
    }

}
