using Hedaya.Application.Certificates.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1.Settings
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
 
    public class CertificatesController : BaseController<CertificatesController>
    {
        [HttpGet("GetAllCertificatesByTraineeId")]
        public async Task<IActionResult> GetAllCertificatesByTraineeId(string id)
        {
            var result = await Mediator.Send(new GetAllCertificatesByTraineeIdQuery { TraineeId = id });

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
