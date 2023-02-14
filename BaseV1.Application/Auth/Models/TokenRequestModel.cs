using System.ComponentModel.DataAnnotations;

namespace BaseV1.Application.Auth.Models
{
    public class TokenRequestModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
