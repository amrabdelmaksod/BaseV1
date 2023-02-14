using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseV1.Application.Auth.Models
{
    public class ForgotPasswordVM
    {
        [Required]
        [EmailAddress(ErrorMessage = "Email Address is not valedit")]
        public string Email { get; set; }
    }
}
