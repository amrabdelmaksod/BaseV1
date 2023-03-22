using Hedaya.Application.Certificates.Models;
using Hedaya.Application.Certificates.Services;
using Hedaya.Application.Interfaces;
using MediatR;

namespace Hedaya.Application.Certificates.Queries
{
    public class GetCertificateByIdApiQuery : IRequest<object>
    {
        public int Id { get; set; }
        public class GetCertificateByIdApiQueryHandler : IRequestHandler<GetCertificateByIdApiQuery, object>
        {
            private readonly ICertificateRepository _certificateRepository;
            private readonly IApplicationDbContext _context;

            public GetCertificateByIdApiQueryHandler(ICertificateRepository certificateRepository, IApplicationDbContext context)
            {
                _certificateRepository = certificateRepository;
                _context = context;
            }

            public async Task<object> Handle(GetCertificateByIdApiQuery request, CancellationToken cancellationToken)
            {

           
                var certificate = await _certificateRepository.GetCertificateById(request.Id);

                if (certificate == null)
                {
                    return new {Message = $"There is no certificate with this id : {request.Id}"};
                }

                return new
                {
                    Result = new CertificateApiDto
                    {
                        Id = certificate.Id,
                        CertificateContent = certificate.ImageUrl
                    }
                };
            }
        }

    }

}
