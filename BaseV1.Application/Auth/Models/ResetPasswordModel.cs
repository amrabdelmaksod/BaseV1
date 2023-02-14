using System.ComponentModel.DataAnnotations;

namespace BaseV1.Application.Auth.Models
{
    public class ResetPasswordModel
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email Address is not valedit")]
        public string user_email { get; set; }
        [Required]
        public string user_password { get; set; }
        [Required]
        public string user_password_confirm { get; set; }

    }
}
