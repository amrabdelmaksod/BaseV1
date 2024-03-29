﻿using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Enrollments.Commands
{
    public class EnrollInProgramCommand : IRequest<object>
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int ProgramId { get; set; }
        public List<int> CourseIds { get; set; } 
        public string UserId { get; set; }

        public class EnrollInProgramCommandHandler : IRequestHandler<EnrollInProgramCommand,object>
        {
            private readonly IApplicationDbContext _context;

            public EnrollInProgramCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<object> Handle(EnrollInProgramCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var traineeId = await _context.Trainees
         .Where(a => a.AppUserId == request.UserId && !a.Deleted)
         .Select(a => a.Id)
         .FirstOrDefaultAsync(cancellationToken);

                    // Get the program by Id
                    var program = await _context.TrainingPrograms
                        .FirstOrDefaultAsync(p => p.Id == request.ProgramId);

                    // Check if the program exists
                    if (program == null)
                    {
                        throw new NotFoundException(nameof(TrainingProgram));
                    }

                    if (traineeId == null)
                    {
                        throw new NotFoundException(nameof(Trainee));
                    }


                   

                    foreach (var courseId in request.CourseIds)
                    {
                        var courseExists = await _context.Courses.Where(a=>a.Id== courseId&&a.TrainingProgramId == request.ProgramId).AsNoTracking().FirstOrDefaultAsync(cancellationToken);
                        var enrollmentExist = await _context.Enrollments.Where(a => !a.Deleted && a.CourseId == courseId && a.TraineeId == traineeId).FirstOrDefaultAsync(cancellationToken);
                        if (enrollmentExist == null&&courseExists!=null)
                        {
                            // Add the enrollment to the database for each course
                            var enrollment = new Enrollment
                            {
                                FullName = request.FullName,
                                MobileNumber = request.MobileNumber,
                                Email = request.Email,
                                TrainingProgramId = request.ProgramId,
                                CourseId = courseId,
                                TraineeId = traineeId,
                                CreatedById = "HedayaAdmin",
                                EnrollmentDate = DateTime.Now,
                                CreationDate = DateTime.Now
                            };

                            _context.Enrollments.Add(enrollment);
                        }
                        else
                        {
                            continue;
                        }

                    }

                    await _context.SaveChangesAsync(cancellationToken);

                    return new {ItemId = program.Id};
                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }

     
            }
        }
    }
}
