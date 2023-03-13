using System.ComponentModel.DataAnnotations;

namespace Hedaya.Application.Auth.Models
{
    public class ForgotPasswordVM
    {
        [Required]
     
        public string MobileNumber { get; set; }
    }
}
