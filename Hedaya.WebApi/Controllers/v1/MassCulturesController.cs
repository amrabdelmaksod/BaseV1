using Hedaya.Application.MassCultures.Models;
using Hedaya.Application.MassCultures.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class MassCulturesController : BaseController<MassCulturesController>
    {

        [HttpGet("GetAllMassCultures")]
        public async Task<ActionResult<MassCulturesResponse>> GetMassCultures([FromQuery] int pageNumber = 1)
        {
            var query = new GetMassCulturesQuery { PageNumber = pageNumber };
            var response = await Mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("MassCultures/{subcategoryId:int}")]
        public async Task<IActionResult> GetMassCulturesBySubcategoryId(int subcategoryId, int pageNumber = 1)
        {
            var query = new GetMassCulturesBySubcategoryIdQuery { SubcategoryId = subcategoryId, PageNumber = pageNumber };
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("Details/{MassCultureId:int}")]
        public async Task<IActionResult> GetMassCultureDetails(int MassCultureId)
        {
            var query = new GetMassCultureByIdQuery { MassCultureId = MassCultureId };
            var result = await Mediator.Send(query);
            if (result is null)
            {
              return  BadRequest(new { error = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"لا توجد ثقافة جماهيرية بهذا المعرف {MassCultureId}" : $"The Mass Culture With This Id {MassCultureId} Is Not Found" });
            }
            return Ok(result);
        }
    }

}
