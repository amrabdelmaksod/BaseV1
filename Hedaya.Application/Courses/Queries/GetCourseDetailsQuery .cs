﻿using Hedaya.Application.Courses.Models;
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
                            IsFav = _context.TraineeCourseFavorites.Any(f => f.CourseId == c.Id && f.TraineeId == traineeId),
                            InstructorName = c.Instructor.GetFullName(),
                            InstructorImageUrl = c.Instructor.ImageUrl ?? "",
                            InstructorDescription = c.Instructor.Description,
                            Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.SubCategory.NameAr : c.SubCategory.NameEn,
                            AboutCourse = c.AboutCourse,
                            CourseCompletionRate = CourseCompletionRate,
                            CourseFeatures = c.CourseFeatures,
                            CourseSyllabus = c.CourseSyllabus,


                            // Topics Of This Course
                            Topics = c.CourseTopics.OrderBy(ct => ct.SortIndex).Select(ct => new CourseTopicDto
                            {
                                Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? ct.NameAr : ct.NameEn,
                                Lessons = ct.Lessons.OrderBy(l => l.SortIndex).Select(l => new LessonDto
                                {
                                    Id = l.Id,
                                    Title = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? l.NameAr : l.NameEn,
                                    Duration = l.Duration
                                }).ToList()
                            }).ToList(),


                          //Forum Of This Course
                            Forum = new ForumDto
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
                            }
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
