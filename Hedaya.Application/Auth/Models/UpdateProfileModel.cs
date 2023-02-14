using System.ComponentModel.DataAnnotations;

namespace Hedaya.Application.Auth.Models
{
    public class UpdateProfileModel
    {
        [MinLength(3, ErrorMessage = "User name loss then 3 char")]
        public string userName { get; set; }
       
        public string userEmail { get; set; }
        public string userPhone { get; set; }
    }
}
