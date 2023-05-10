using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Enrollments.Commands
{
    public class EnrollInCourseCommand : IRequest<object>
    {
       
       
        public int CourseId { get; set; }
        public string UserId { get; set; }

        public class EnrollInCourseCommandHandler : IRequestHandler<EnrollInCourseCommand,object>
        {
            private readonly IApplicationDbContext _context;

            public EnrollInCourseCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(EnrollInCourseCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var traineeId = await _context.Trainees
                         .Where(a => a.AppUserId == request.UserId && !a.Deleted)
                         .Select(a => a.Id)
                         .FirstOrDefaultAsync(cancellationToken);

                    if (traineeId == null)
                    {
                        throw new NotFoundException(nameof(traineeId));
                    }

                    var Trainee = await _context.Trainees.Where(a=>a.Id == traineeId).FirstOrDefaultAsync();

                    // Get the program by Id
                    var Course = await _context.Courses
                        .FirstOrDefaultAsync(p => p.Id == request.CourseId);

                    // Check if the program exists
                    if (Course == null)
                    {
                        throw new NotFoundException(nameof(TrainingProgram));
                    }

                    if (Trainee == null )
                    {
                        throw new NotFoundException(nameof(Trainee));
                    }

                  
                        var enrollmentExist = await _context.Enrollments.Where(a => !a.Deleted && a.CourseId == request.CourseId && a.TraineeId == traineeId).FirstOrDefaultAsync(cancellationToken);
                        if (enrollmentExist == null)
                        {
                            // Add the enrollment to the database for each course
                            var enrollment = new Enrollment
                            {
                                
                                FullName = Trainee.AppUser.FullName??"",
                                MobileNumber = Trainee.AppUser.UserName??"",
                                Email = Trainee.AppUser.Email??"",
                                TrainingProgramId = Course.TrainingProgramId,
                                CourseId = request.CourseId,
                                TraineeId = traineeId,
                                CreatedById = "HedayaAdmin",
                                EnrollmentDate = DateTime.Now,
                                CreationDate = DateTime.Now
                            };

                            _context.Enrollments.Add(enrollment);
                        await _context.SaveChangesAsync(cancellationToken);

                        return new { ItemId = Course.Id, Message = "Enrolled Successfully" };
                    }

                    else
                    {

                        return new { ItemId = Course.Id, Message = " Already Enrolled" };
                    }
                   


                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }


            }
        }
    }
}
