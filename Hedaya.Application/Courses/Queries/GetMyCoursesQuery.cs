using Hedaya.Application.Courses.Models;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Courses.Queries
{
    public class GetMyCoursesQuery : IRequest<object>
    {
        public int PageNumber { get; set; }

        public string UserId { get; set; }
        public int TypeId { get; set; }

        public class GetMyCoursesQueryHandler : IRequestHandler<GetMyCoursesQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetMyCoursesQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetMyCoursesQuery request, CancellationToken cancellationToken)
            {

               
                var PageSize = 10;

                var traineeId = await _context.Trainees
                     .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                     .Select(a => a.Id)
                     .FirstOrDefaultAsync(cancellationToken);

                var paginatedList = new PaginatedList<MyCoursesDto>();

                var totalCount = 0;
                var totalPages = 0;

                var MyCourses = new List<MyCoursesDto>();

                switch (request.TypeId)
                {
                    case 0://كل الدورات

                        MyCourses = await _context.Enrollments.Where(c => !c.Deleted && c.TraineeId == traineeId).Select(c => new MyCoursesDto
                        {

                            Id = c.CourseId,
                            Title = c.Course.TitleAr,
                            StartDate = c.Course.StartDate.ToString("d"),
                            Duration = c.Course.Duration,
                            ImageUrl = c.Course.ImageUrl,
                            IsFav = _context.TraineeCourseFavorites.Any(f => f.CourseId == c.Id && f.TraineeId == traineeId),
                            InstructorName = c.Course.Instructor.GetFullName(),
                            InstructorImageUrl = c.Course.Instructor.ImageUrl,
                            Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.Course.SubCategory.NameAr : c.Course.SubCategory.NameEn,
                            TrainingProgramId = c.TrainingProgramId,
                            CategoryId = c.Course.SubCategoryId,
                            VideoUrl = c.Course.VideoUrl,
                            EndDate = c.Course.EndDate.ToString("d"),
                            LessonsCount = _context.Lessons.Where(a=>a.CourseTopic.CourseId == c.Id).Count(),
                            CertificateImageUrl = _context.Certificates.Where(a=>a.TraineeId == traineeId && a.CourseId == c.CourseId).Select(certificate=>certificate.ImageUrl).FirstOrDefault()??"",
                            TypeId = c.Course.EndDate >= DateTime.Now.Date ? 1 : 2,
                            Result = new Random().Next(10, 100),
                            TestsCount =_context.CourseTests.Where(a=>a.CourseId == c.CourseId).Count() ,
                            CompletionPercentage = new Random().Next(10, 100),
                        }).ToListAsync(cancellationToken);
                         totalCount = await _context.Enrollments.Where(c => !c.Deleted && c.TraineeId == traineeId).CountAsync();
                         totalPages = (int)Math.Ceiling((double)totalCount / PageSize);

                        paginatedList = new PaginatedList<MyCoursesDto>(MyCourses, totalCount, request.PageNumber, PageSize, totalPages);
                        break;

                    case 1: // الدورات الحالية


                        MyCourses = await _context.Enrollments.Where(c => !c.Deleted && c.TraineeId == traineeId && c.Course.EndDate>=DateTime.Now.Date).Select(c => new MyCoursesDto
                        {

                            Id = c.CourseId,
                            Title = c.Course.TitleAr,
                            StartDate = c.Course.StartDate.ToString("d"),
                            Duration = c.Course.Duration,
                            ImageUrl = c.Course.ImageUrl,
                            IsFav = _context.TraineeCourseFavorites.Any(f => f.CourseId == c.Id && f.TraineeId == traineeId),
                            InstructorName = c.Course.Instructor.GetFullName(),
                            InstructorImageUrl = c.Course.Instructor.ImageUrl,
                            Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.Course.SubCategory.NameAr : c.Course.SubCategory.NameEn,
                            TrainingProgramId = c.TrainingProgramId,
                            CategoryId = c.Course.SubCategoryId,
                            VideoUrl = c.Course.VideoUrl,
                            EndDate = c.Course.EndDate.ToString("d"),
                            LessonsCount = _context.Lessons.Where(a => a.CourseTopic.CourseId == c.Id).Count(),
                            CertificateImageUrl = _context.Certificates.Where(a => a.TraineeId == traineeId && a.CourseId == c.CourseId).Select(certificate => certificate.ImageUrl).FirstOrDefault() ?? "",
                            TypeId = request.TypeId,
                            Result = new Random().Next(10, 100),
                            TestsCount = _context.CourseTests.Where(a => a.CourseId == c.CourseId).Count(),
                            CompletionPercentage = new Random().Next(10, 100),
                        }).ToListAsync(cancellationToken);
                        totalCount = await _context.Enrollments.Where(c => !c.Deleted && c.TraineeId == traineeId && c.Course.EndDate >= DateTime.Now.Date).CountAsync();
                        totalPages = (int)Math.Ceiling((double)totalCount / PageSize);

                        paginatedList = new PaginatedList<MyCoursesDto>(MyCourses, totalCount, request.PageNumber, PageSize, totalPages);

                        break;

                    case 2: // الدورات المنتهية
                        MyCourses = await _context.Enrollments.Where(c => !c.Deleted && c.TraineeId == traineeId && c.Course.EndDate <= DateTime.Now.Date).Select(c => new MyCoursesDto
                        {

                            Id = c.CourseId,
                            Title = c.Course.TitleAr,
                            StartDate = c.Course.StartDate.ToString("d"),
                            Duration = c.Course.Duration,
                            ImageUrl = c.Course.ImageUrl,
                            IsFav = _context.TraineeCourseFavorites.Any(f => f.CourseId == c.Id && f.TraineeId == traineeId),
                            InstructorName = c.Course.Instructor.GetFullName(),
                            InstructorImageUrl = c.Course.Instructor.ImageUrl,
                            Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.Course.SubCategory.NameAr : c.Course.SubCategory.NameEn,
                            TrainingProgramId = c.TrainingProgramId,
                            CategoryId = c.Course.SubCategoryId,
                            VideoUrl = c.Course.VideoUrl,
                            EndDate = c.Course.EndDate.ToString("d"),
                            LessonsCount = _context.Lessons.Where(a => a.CourseTopic.CourseId == c.Id).Count(),
                            CertificateImageUrl = _context.Certificates.Where(a => a.TraineeId == traineeId && a.CourseId == c.CourseId).Select(certificate => certificate.ImageUrl).FirstOrDefault() ?? "",
                            TypeId = request.TypeId,
                            Result = new Random().Next(10, 100),
                            TestsCount = _context.CourseTests.Where(a => a.CourseId == c.CourseId).Count(),
                            CompletionPercentage = new Random().Next(10, 100),
                        }).ToListAsync(cancellationToken);
                        totalCount = await _context.Enrollments.Where(c => !c.Deleted && c.TraineeId == traineeId && c.Course.EndDate <= DateTime.Now.Date).CountAsync();
                        totalPages = (int)Math.Ceiling((double)totalCount / PageSize);

                        paginatedList = new PaginatedList<MyCoursesDto>(MyCourses, totalCount, request.PageNumber, PageSize, totalPages);
                        break;

                    default:
                        break;
                }

               


              





                return new { Result = paginatedList };
            }
        }
    }
}
