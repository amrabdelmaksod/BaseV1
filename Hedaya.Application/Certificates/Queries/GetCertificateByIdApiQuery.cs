using Hedaya.Application.Certificates.Models;
using Hedaya.Application.Certificates.Services;
using Hedaya.Application.Interfaces;
using Hedaya.Domain.Entities;
using MediatR;
using System.Net;

namespace Hedaya.Application.Certificates.Queries
{
    public class GetCertificateByIdApiQuery : IRequest<CertificateApiDto>
    {
        public int Id { get; set; }
        public class GetCertificateByIdApiQueryHandler : IRequestHandler<GetCertificateByIdApiQuery, CertificateApiDto>
        {
            private readonly ICertificateRepository _certificateRepository;
            private readonly IApplicationDbContext _context;

            public GetCertificateByIdApiQueryHandler(ICertificateRepository certificateRepository, IApplicationDbContext context)
            {
                _certificateRepository = certificateRepository;
                _context = context;
            }

            public async Task<CertificateApiDto> Handle(GetCertificateByIdApiQuery request, CancellationToken cancellationToken)
            {

           
                var certificate = await _certificateRepository.GetCertificateById(request.Id);

                if (certificate == null)
                {
                    return null;
                }

                return new CertificateApiDto
                {
                    Id = certificate.Id,
                    CertificateContent = certificate.CertificateContent
                };
            }
        }

    }

}
