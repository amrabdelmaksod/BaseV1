using Hedaya.Application.Courses.Models;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Application.TrainingPrograms.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.TrainingPrograms.Queries
{
    public class GetMyProgramsQuery : IRequest<object>
    {

        public int PageNumber { get; set; }

        public string UserId { get; set; }

        public class GetMyProgramsQueryHandler : IRequestHandler<GetMyProgramsQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetMyProgramsQueryHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(GetMyProgramsQuery request, CancellationToken cancellationToken)
            {
                try
                {
                    var PageSize = 10;

                    var traineeId = await _context.Trainees
                         .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                         .Select(a => a.Id)
                         .FirstOrDefaultAsync(cancellationToken);

                    var paginatedList = new PaginatedList<MyProgramsDto>();

                    var totalCount = 0;
                    var totalPages = 0;

                    var MyPrograms = new List<MyProgramsDto>();
                    var MyCourses = new List<MyCoursesDto>();



                    var programsIds =  _context.Enrollments.Where(c => !c.Deleted && c.TraineeId == traineeId).CustomDistinctBy(a => a.TrainingProgramId).Select(a => a.TrainingProgramId).ToList();


                    foreach (var item in programsIds)
                    {
                        var myProgram = await _context.TrainingPrograms.Where(a => a.Id == item).Select(p => new MyProgramsDto
                        {

                            Id = p.Id,
                            StartDate = p.StartDate.ToString("d"),
                            EndDate = p.EndDate.ToString("d"),
                            ImageUrl = p.ImgUrl,
                            Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? p.TitleAr : p.TitleEn,
                            CourseCount = _context.Courses.Where(a => a.TrainingProgramId == p.Id).Count(),
                            TestsCount = _context.CourseTests.Where(a => a.Course.TrainingProgramId == p.Id).Count(),
                            CompletionRate = 100,
                        }).FirstOrDefaultAsync(cancellationToken);


                        var myProgramCoursesIds = await _context.Enrollments.Where(a => a.TrainingProgramId == myProgram.Id && a.TraineeId == traineeId).Select(a => a.CourseId).ToListAsync();

                        foreach (var courseId in myProgramCoursesIds)
                        {
                            var myCourse = await _context.Courses.Where(a => a.Id == courseId).Select(c => new MyCoursesDto
                            {
                                Id = c.Id,
                                Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.TitleAr : c.TitleEn,
                                StartDate = c.StartDate.ToString("d"),
                                EndDate = c.EndDate.ToString("d"),
                                Duration = c.Duration,
                                ImageUrl = c.ImageUrl,
                                VideoUrl = c.VideoUrl,
                                IsFav = _context.TraineeCourseFavorites.Any(f => f.CourseId == c.Id && f.TraineeId == traineeId),
                                InstructorName = c.Instructor.GetFullName(),
                                InstructorImageUrl = c.Instructor.ImageUrl,
                                Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.SubCategory.NameAr : c.SubCategory.NameEn,
                                CategoryId = c.SubCategoryId,
                                TrainingProgramId = c.TrainingProgramId,
                                LessonsCount = _context.Lessons.Where(a => a.CourseTopic.CourseId == c.Id).Count(),
                                Result = new Random().Next(10, 100),
                                CertificateImageUrl = _context.Certificates.Where(a => a.CourseId == c.Id && a.TraineeId == traineeId).Select(a => a.ImageUrl).FirstOrDefault() ?? "",
                                TypeId = c.EndDate >= DateTime.Now.Date ? 1 : 2,

                            }).FirstOrDefaultAsync(cancellationToken);

                            if (myCourse != null)
                            {
                                MyCourses.Add(myCourse);
                            }
                        }



                        if (myProgram != null)
                        {
                            myProgram.Courses = MyCourses;
                            MyPrograms.Add(myProgram);
                        }


                    }





                    totalCount =  _context.Enrollments.Where(c => !c.Deleted && c.TraineeId == traineeId).CustomDistinctBy(a=>a.TrainingProgramId).Count();
                    totalPages = (int)Math.Ceiling((double)totalCount / PageSize);





                    paginatedList = new PaginatedList<MyProgramsDto>(MyPrograms, totalCount, request.PageNumber, PageSize, totalPages);




                    return new { Result = paginatedList };
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }


               
            }
        }
    }
}
