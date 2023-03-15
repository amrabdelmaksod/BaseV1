using Hedaya.WebApi.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class HomeController : BaseController<HomeController>
    {
        private readonly ILoggerManager _loggerManager;
        public HomeController(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

      
    }
}
