using System.ComponentModel.DataAnnotations;

namespace Hedaya.Application.Auth.Models
{
    public class TokenRequestModel
    {
     
        public string Mobile { get; set; }
     
        public string Password { get; set; }
    }
}
