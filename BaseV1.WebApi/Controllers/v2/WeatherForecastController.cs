using API.Errors;
using BaseV1.Application.Tests.Commands;
using BaseV1.WebApi.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BaseV1.WebApi.Controllers.v2
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("2.0")]
  
    public class WeatherForecastController : BaseController<WeatherForecastController>
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILoggerManager _logger;

        public WeatherForecastController(ILoggerManager logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            _logger.LogError("Test Logger");
            _logger.LogInfo("Test Log Info");
            _logger.LogWarn("Test Log Warning");
     

        

            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();


        }

        [HttpPost(Name = "Create")]

        public async Task<IActionResult> Create(CreateTestCommand command)
           {
            _logger.LogInfo("Test Logger Create");
            CreateTestCommandValidator validationRules= new CreateTestCommandValidator();
            var validateRes = validationRules.Validate(command);
            if(validateRes.IsValid)
            {
                var x = await Mediator.Send(command);
                return Ok(x);
            }
            else
            {
                foreach (var failer in validateRes.Errors)
                {
                    ModelState.AddModelError(failer.PropertyName, failer.ErrorMessage);
                }

                return CustomBadRequest.CustomModelStateErrorResponse(ModelState);

                
            }
             
          
        }

     

    }
}
