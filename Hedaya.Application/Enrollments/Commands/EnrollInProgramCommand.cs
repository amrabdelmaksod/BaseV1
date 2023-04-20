using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Enrollments.Commands
{
    public class EnrollInProgramCommand : IRequest
    {
        public string FullName { get; set; }
        public string MobileNumber { get; set; }
        public string Email { get; set; }
        public int ProgramId { get; set; }
        public string UserId { get; set; }

        public class EnrollInProgramCommandHandler : IRequestHandler<EnrollInProgramCommand>
        {
            private readonly IApplicationDbContext _context;

            public EnrollInProgramCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<Unit> Handle(EnrollInProgramCommand request, CancellationToken cancellationToken)
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

                // Add the enrollment to the database
                var enrollment = new Enrollment
                {
                    FullName = request.FullName,
                    MobileNumber = request.MobileNumber,
                    Email = request.Email,
                    TrainingProgramId = request.ProgramId,
                    TraineeId = traineeId,
                    CreatedById = "hedaya",
                    EnrollmentDate = DateTime.Now,
                    CreationDate = DateTime.Now,
                    
                };

                _context.Enrollments.Add(enrollment);
                await _context.SaveChangesAsync(cancellationToken);

                return Unit.Value;
            }
        }

    }

}
