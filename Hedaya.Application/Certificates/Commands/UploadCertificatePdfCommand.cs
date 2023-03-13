using Hedaya.Application.Certificates.Services;
using Hedaya.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Certificates.Commands
{
    public class UploadCertificatePdfCommand : IRequest<Unit>
    {
        public int CertificateId { get; set; }
        public IFormFile PdfFile { get; set; }

        public class UploadCertificatePdfCommandHandler : IRequestHandler<UploadCertificatePdfCommand>
        {
            private readonly ICertificateRepository _certificateRepository;

            public UploadCertificatePdfCommandHandler(ICertificateRepository certificateRepository)
            {
                _certificateRepository = certificateRepository;
            }

            public async Task<Unit> Handle(UploadCertificatePdfCommand request, CancellationToken cancellationToken)
            {
                var certificate = await _certificateRepository.GetCertificateById(request.CertificateId);
                

                if (certificate == null)
                {
                    throw new NotFoundException(nameof(Certificate));
                }

                //certificate.CertificateContent = request.PdfFile;

                //await _certificateRepository.UpdateAsync(certificate);

                return Unit.Value;
            }
        }



    }

}
