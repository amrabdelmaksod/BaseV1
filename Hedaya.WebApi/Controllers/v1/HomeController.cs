using Hedaya.WebApi.Interfaces;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Hedaya.WebApi.Controllers.v1
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class HomeController : BaseController<HomeController>
    {
        private readonly ILoggerManager _loggerManager;
        public HomeController(ILoggerManager loggerManager)
        {
            _loggerManager = loggerManager;
        }

        [HttpPost]
        public async Task<IActionResult> SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(

                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            var x = CultureInfo.CurrentCulture.TwoLetterISOLanguageName;

            return LocalRedirect(returnUrl);
           
        }
    }
}
