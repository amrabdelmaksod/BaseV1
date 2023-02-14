using System.ComponentModel.DataAnnotations;

namespace Hedaya.Application.Auth.Models
{
    public class RegisterModel
    {
        
        public string UserName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }     

        [Compare("Password")]
        public string ConfirmPassword { get; set; }

       
         
    }
}
