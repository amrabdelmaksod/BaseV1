using Hedaya.Domain.Entities;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace Hedaya.Application.Services
{
    // Define the IPdfService interface
    public interface IPdfService
    {
        Task<byte[]> GenerateCertificatePdfAsync(Certificate certificate);
    }

    // Implement the IPdfService interface using iTextSharp
    public class PdfService : IPdfService
    {
        public async Task<byte[]> GenerateCertificatePdfAsync(Certificate certificate)
        {
            // Create a new MemoryStream to store the PDF file
            var pdfStream = new MemoryStream();

            // Create a new PDF document
            var document = new Document();

            try
            {
                // Use the MemoryStream and the Document to create a new PDF writer
                var writer = PdfWriter.GetInstance(document, pdfStream);

                // Open the document
                document.Open();

                // Add a new page to the document
                document.NewPage();

                // Add a new paragraph to the document with the certificate information
                var paragraph = new Paragraph($"Certificate ID: {certificate.Id}");
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

            // Convert the MemoryStream to a byte array and return it
            return pdfStream.ToArray();
        }
    }

}
