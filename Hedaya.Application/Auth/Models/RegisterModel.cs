using Hedaya.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hedaya.Application.Auth.Models
{
    public class RegisterModel
    {       
        public string FullName { get; set; }      
        public string PhoneNumber { get; set; }        
        public string Email { get; set; }      
        public Nationality Country { get; set; }       
        public string Password { get; set; }     
        public string ConfirmPassword { get; set; }
    }

}
