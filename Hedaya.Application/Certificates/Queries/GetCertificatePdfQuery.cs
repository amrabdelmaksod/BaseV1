using Hedaya.Application.Certificates.Commands;
using Hedaya.Application.Certificates.Services;
using Hedaya.Domain.Entities;
using MediatR;
using SendGrid.Helpers.Errors.Model;

namespace Hedaya.Application.Certificates.Queries
{
    public class GetCertificatePdfQuery : IRequest<MemoryStream>
    {
        public int CertificateId { get; set; }
    }

    public class GetCertificatePdfQueryHandler : IRequestHandler<GetCertificatePdfQuery, MemoryStream>
    {
        private readonly ICertificateRepository _certificateRepository;
        private readonly IMediator _mediator;

        public GetCertificatePdfQueryHandler(ICertificateRepository certificateRepository, IMediator mediator)
        {
            _certificateRepository = certificateRepository;
            _mediator = mediator;
        }

        public async Task<MemoryStream> Handle(GetCertificatePdfQuery request, CancellationToken cancellationToken)
        {
            var certificate = await _certificateRepository.GetCertificateById(request.CertificateId);

            if (certificate == null)
            {
                throw new NotFoundException(nameof(Certificate));
            }

            var command = new GenerateCertificatePdfCommand { Certificate = certificate };
            var pdfStream = await _mediator.Send(command);

            return pdfStream;
        }
    }

}
