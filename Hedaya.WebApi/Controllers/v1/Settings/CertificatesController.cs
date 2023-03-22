using Hedaya.Application.Certificates.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
 
    public class CertificatesController : BaseController<CertificatesController>
    {
        [HttpGet("GetAllCertificatesByTraineeId")]
        public async Task<IActionResult> GetAllCertificatesByTraineeId()
        {
            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            var result = await Mediator.Send(new GetAllCertificatesByTraineeIdQuery { UserId = userId });

            return Ok(result);
        }

        //[HttpGet("{id}/pdf")]
        //public async Task<IActionResult> GetCertificatePdf(int id)
        //{
       
        //    var query = new GetCertificatePdfQuery { CertificateId = id };
        //    var pdfStream = await Mediator.Send(query);

        //    return File(pdfStream, "application/pdf", $"certificate-{id}.pdf");
        //}

    }
}
