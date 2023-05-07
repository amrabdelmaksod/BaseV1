using Hedaya.Application.Courses.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Courses.Queries
{
    // Query
    public class GetCourseDetailsQuery : IRequest<object>
    {
        public int CourseId { get; set; }
        public string UserId { get; set; }

        // Query Handler
        public class GetCourseDetailsQueryHandler : IRequestHandler<GetCourseDetailsQuery, object>
        {
            private readonly IApplicationDbContext _context;


            public GetCourseDetailsQueryHandler(IApplicationDbContext dbContext)
            {
                _context = dbContext;

            }

            public async Task<object> Handle(GetCourseDetailsQuery request, CancellationToken cancellationToken)
            {
                try
                {

                    var traineeId = await _context.Trainees
                    .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                    .Select(a => a.Id)
                    .FirstOrDefaultAsync(cancellationToken);

                    var CourseCompletionRate = await GetCourseCompletionRateAsync(request.CourseId, traineeId);

                   var IsEnrolled = await _context.Enrollments.AnyAsync(a=>!a.Deleted && a.CourseId == request.CourseId &&a.TraineeId == traineeId);

                    var course = await _context.Courses
                        .Where(c => c.Id == request.CourseId)
                        .Include(c => c.Instructor)
                        .Include(c => c.SubCategory)
                        .Include(c => c.Forum).ThenInclude(a => a.Posts).ThenInclude(a => a.Trainee).ThenInclude(a => a.Comments).ThenInclude(a => a.Trainee)
                        .Select(c => new CourseDetailsDto
                        {
                            //Details of this course

                            Id = c.Id,
                            Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.TitleAr : c.TitleEn,
                            StartDate = c.StartDate.ToString("d"),
                            Duration = c.Duration,
                            ImageUrl = c.ImageUrl ?? "",
                            VideoUrl = c.VideoUrl ??"",
                            IsFav = _context.TraineeCourseFavorites.Any(f => f.CourseId == c.Id && f.TraineeId == traineeId),
                            InstructorId = c.Instructor.Id,
                            InstructorName = c.Instructor.GetFullName(),
                            InstructorImageUrl = c.Instructor.ImageUrl ?? "",
                            InstructorDescription = c.Instructor.Description,
                            Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.SubCategory.NameAr : c.SubCategory.NameEn,
                            CategoryId = c.SubCategoryId,
                            TrainingProgramId = c.TrainingProgramId,
                            AboutCourse = c.AboutCourse,
                            CourseCompletionRate = CourseCompletionRate,
                            CourseFeatures = c.CourseFeatures,
                            CourseSyllabus = c.CourseSyllabus,
                            IsEnrolled = IsEnrolled,

                            // Topics Of This Course
                            Topics = c.CourseTopics.OrderBy(ct => ct.SortIndex).Select(ct => new CourseTopicDto
                            {
                                Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? ct.NameAr : ct.NameEn,
                                Lessons = ct.Lessons.OrderBy(l => l.SortIndex).Select(l => new LessonDto
                                {
                                    Id = l.Id,
                                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? l.NameAr : l.NameEn,
                                    Duration = l.Duration,
                                    VideoUrl = l.VideoUrl??"",
                                    IsCompleeted =  _context.TraineeLessons.Any(t => t.TraineeId == traineeId && t.LessonId == l.Id && t.IsCompleted)
                                }).ToList()
                            }).ToList(),


                            //Forum Of This Course
                            Forum = c.Forum != null ? new ForumDto
                            {
                                Id = c.Forum.Id,
                                Posts = c.Forum.Posts.Select(p => new PostDto
                                {
                                    Id = p.Id,
                                    TraineeName = p.Trainee.FullName,
                                    Text = p.Text,
                                    ImageUrl = p.ImagePath ?? "",
                                    Coments = p.Comments.Select(c => new CommentDto
                                    {
                                        Id = c.Id,
                                        TraineeName = c.Trainee.FullName ?? "",
                                        Text = c.Text ?? "",
                                        ImgUrl = c.ImagePath ?? ""
                                    }).ToList()
                                }).ToList()
                            } : "",
                        
                         
                    Tests = c.CourseTests.Select(t => new CourseTestDto
                            {
                                Id = t.Id,
                                Title = t.Title,
                                QuestionsCount = t.Questions.Count,
                                Questions = t.Questions.Select(q => new QuestionDto
                                {
                                    Id = q.Id,
                                    Text = q.Text,
                                    Type = q.Type,
                                    Answers = q.Answers.Select(a => new AnswerDto
                                    {
                                        Id = a.Id,
                                        Text = a.Text,
                                        IsCorrect = a.IsCorrect
                                    }).ToList()
                                }).ToList()
                            }).ToList()


                        })
                        .FirstOrDefaultAsync(cancellationToken);



                    // Get Course Completion Rate Based On Trainee 
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

                    return new { result = course };
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

            }

        }



   


    }
}
