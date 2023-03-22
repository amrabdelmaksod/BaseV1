using Hedaya.Application.MethodologicalExplanations.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class MethodologicalExplanationController : BaseController<MethodologicalExplanationController>
    {


        public MethodologicalExplanationController()
        {

        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll([FromQuery] int pageNumber)
        {
            var query = new GetAllMethodlogicalExplanationQuery { PageNumber = pageNumber };
            var result = await Mediator.Send(query);
            return Ok(result);
        }


        [HttpGet("MethodlogicalExplanation/{subcategoryId:int}")]
        public async Task<IActionResult> GetMassCulturesBySubcategoryId(int subcategoryId, int pageNumber = 1)
        {
            var query = new GetMethodlogicalExplanationsBySubCategoryIdQuery { SubCategoryId = subcategoryId, PageNumber = pageNumber };
            var result = await Mediator.Send(query);

            return Ok(result);
        }

        [HttpGet("FilterMethodologicalExplanations")]
        public async Task<IActionResult> FilterMethodologicalExplanations(
            [FromQuery] string? SearchKeyword,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int? subCategoryId = null,
            [FromQuery] bool sortByDurationAscending = true )

        {
            var query = new FilterMethodologicalExplanationsQuery
            {
                PageNumber = pageNumber,
                SubCategoryId = subCategoryId,
                SortByDurationAscending = sortByDurationAscending,
                searchKeyword = SearchKeyword,
            };
            var result = await Mediator.Send(query);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<object>> GetMethodlogicalExplanationById(int id)
        {
            var query = new GetMethodlogicalExplanationByIdQuery { Id = id };
            var result = await Mediator.Send(query);
            if (result == null)
                return BadRequest(CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"{id}عفوا لا يوجد شروحات منهجية بهذا المعرف" : $"Sorry The Explanation With this id : {id} is not found");
            return Ok(result);
        }



    }

}
