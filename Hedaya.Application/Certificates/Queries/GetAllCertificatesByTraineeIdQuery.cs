using Hedaya.Application.Certificates.Models;
using Hedaya.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Hedaya.Application.Certificates.Queries
{
    public class GetAllCertificatesByTraineeIdQuery : IRequest<object>
    {
        public string TraineeId { get; set; }
        public class Handler : IRequestHandler<GetAllCertificatesByTraineeIdQuery,object>
        {


            private readonly IApplicationDbContext _context;
            public Handler(IApplicationDbContext context)
            {

                _context = context;
            }

            public async Task<object> Handle(GetAllCertificatesByTraineeIdQuery request, CancellationToken cancellationToken)
            {
                try
                {
                                   


                    var Certificates = await _context.Certificates.Include(a => a.Trainee).Include(a => a.Course).ThenInclude(a => a.Instructor).Where(a => !a.Deleted && a.TraineeId == request.TraineeId)

                        .Select(a => new CertificateDto
                        {
                            Id = a.Id,
                            CourseTitle = a.Course.Title,
                            InstructorName = a.Course.Instructor.GetFullName(),
                            TraineeName = a.Trainee.FullName,
                            TraineeCode = a.TraineeId
                        })
                       .ToListAsync();


                    return new { Result = Certificates }  ;

                }
                catch (Exception ex)
                {

                    throw new Exception(ex.Message);
                }
            }
        }
    }
}
