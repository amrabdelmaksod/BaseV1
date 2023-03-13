using System.ComponentModel.DataAnnotations;

namespace Hedaya.Application.Auth.Models
{
    public class TokenRequestModel
    {
        [Required]
     
        public string Mobile { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
