using Hedaya.WebApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1
{
    [Route("api/[controller]")]
    [ApiController]
    public class EsraaController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetUser()
        {
            var user = new Esraa
            {
                Email = "Esraa@gmail.com",
                JobTitle = "Software Engineer",
                ProfilePicture = "./images/profile.jpg",
                Name = "Esraa",
                Facebook = "https://www.facebook.com/esraa",
                Whatsapp = "https://wa.me/1234567890",
                github = "https://www.github.com/channel/UC-abc123",
                LinkeIn = "https://www.linkedin.com/in/esraa",
                Instagram = "https://www.instagram.com/esraa",
                Twitter = "https://t.me/esraa"
            };
            return Ok(user);
        }
    }
}
