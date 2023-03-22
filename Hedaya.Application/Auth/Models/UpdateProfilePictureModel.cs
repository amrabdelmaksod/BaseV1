using Microsoft.AspNetCore.Http;

namespace Hedaya.Application.Auth.Models
{
    public class UpdateProfilePictureModel
    {
        public IFormFile ProfilePicture { get; set; }
    }
}
