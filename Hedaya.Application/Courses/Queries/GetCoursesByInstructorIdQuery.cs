using Hedaya.Application.Courses.Models;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace Hedaya.Application.Courses.Queries
{
    public class GetCoursesByInstructorIdQuery : IRequest<object>
    {
        public string InstructorId { get; set; }
        public string UserId { get; set; }
        public int PageNumber { get; set; }
        

        public class GetInstructorCoursesQueryHandler : IRequestHandler<GetCoursesByInstructorIdQuery, object>
        {
            private readonly IApplicationDbContext _context;

            public GetInstructorCoursesQueryHandler(IApplicationDbContext dbContext)
            {
                _context = dbContext;
            }

            public async Task<object> Handle(GetCoursesByInstructorIdQuery request, CancellationToken cancellationToken)
            {
                var PageSize = 10;

                var traineeId = await _context.Trainees
                   .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                   .Select(a => a.Id)
                   .FirstOrDefaultAsync(cancellationToken);

                var instructor = await _context.Instructors
                    .Where(a => a.Id == request.InstructorId && !a.Deleted)
                    .Select(a => new InstructorDto { Id = a.Id, Description = a.Description, Name = a.GetFullName() })
                    .FirstOrDefaultAsync();

                var totalCount = await _context.Courses.CountAsync(c => c.InstructorId == request.InstructorId, cancellationToken);

                var instructorCourses = await _context.Courses
                    .Where(c => c.InstructorId == request.InstructorId)
                    .OrderByDescending(c => c.Id)
                    .Skip((request.PageNumber - 1) * PageSize)
                    .Take(PageSize)
                    .Select(c => new InstructorCoursesDto
                    {
                        Id = c.Id,
                        Title = c.TitleAr,
                        StartDate = c.StartDate.ToString("yyyy-MM-dd"),
                        Duration = c.Duration,
                        ImageUrl = c.ImageUrl,
                        IsFav = _context.TraineeCourseFavorites.Any(f => f.CourseId == c.Id && f.TraineeId == traineeId),
                        Category = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? c.SubCategory.NameAr : c.SubCategory.NameEn
                    })
                    .ToListAsync(cancellationToken);

                var totalPages = (int)Math.Ceiling((double)totalCount / PageSize);

                var response = new InstructoCoursesLiDto { Instructor = instructor, Courses = instructorCourses };
                var paginatedList = new PaginatedList<InstructoCoursesLiDto>(response, totalCount, request.PageNumber, PageSize, totalPages);

                return new { result = paginatedList };
            }
        }
    }
}
