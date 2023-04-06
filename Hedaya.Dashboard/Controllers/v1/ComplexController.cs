using Hedaya.Application.Complexes.Commands;
using Hedaya.Application.Complexes.Commands.Create;
using Hedaya.Application.Complexes.Commands.Delete;
using Hedaya.Application.Complexes.Commands.Update;
using Hedaya.Application.Complexes.DTOs;
using Hedaya.Application.Complexes.Queries.GetComplex;
using Hedaya.Application.Users.Commands.CreateUser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.Dashboard.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class ComplexController : BaseController<ComplexController>
    {





        [HttpGet("GetComplexData")]
        public async Task<ActionResult<ComplexDetailsDto>> GetComplexData()
        {
            var query = new GetComplexQuery();
            var result = await Mediator.Send(query);

            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }



        [HttpPost("CreateComplex")]
        public async Task<ActionResult<int>> CreateComplex(CreateComplexCommand command)
        {
            var validationResult = await new CreateComplexCommandValidator().ValidateAsync(command);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

           
            var id = await Mediator.Send(command);
                   return Ok(id);       
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] UpdateComplexCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest();
            }

            await Mediator.Send(command);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await Mediator.Send(new DeleteComplexCommand { Id = id });

            return NoContent();
        }

    }
}
