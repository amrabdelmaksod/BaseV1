using Hedaya.Application.Certificates.Services;
using Hedaya.Domain.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;
using MediatR;


namespace Hedaya.Application.Certificates.Commands
{
    public class GenerateCertificatePdfCommand : IRequest<MemoryStream>
    {
        public Certificate Certificate { get; set; }
    }

    public class GenerateCertificatePdfCommandHandler : IRequestHandler<GenerateCertificatePdfCommand, MemoryStream>
    {
        private readonly ICertificateRepository _certificateRepository;

        public GenerateCertificatePdfCommandHandler(ICertificateRepository certificateRepository)
        {
            _certificateRepository = certificateRepository;
        }

        public async Task<MemoryStream> Handle(GenerateCertificatePdfCommand request, CancellationToken cancellationToken)
        {
            // Create a new MemoryStream to store the PDF file
            var pdfStream = new MemoryStream();

            // Create a new PDF document
            var document = new Document(PageSize.A4, 50, 50, 25, 25);


            try
            {
                // Use the MemoryStream and the Document to create a new PDF writer
                var writer = PdfWriter.GetInstance(document, pdfStream);

                // Open the document
                document.Open();

                // Add a new page to the document
                document.NewPage();

                // Add a new paragraph to the document with the certificate information
                var paragraph = new Paragraph($"Certificate ID: {request.Certificate.Id}");
                document.Add(paragraph);

                // You can add more information to the PDF file, such as the trainee name, course name, and so on

                // Close the document
                document.Close();
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during PDF generation
                throw new Exception("Error generating PDF file", ex);
            }

            // Reset the position of the MemoryStream to the beginning
            pdfStream.Position = 0;

            // Return the MemoryStream containing the PDF file
            return pdfStream;
        }


    }

}
