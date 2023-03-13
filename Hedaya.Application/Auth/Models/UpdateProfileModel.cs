using Hedaya.Domain.Enums;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Hedaya.Application.Auth.Models
{
    public class UpdateProfileModel
    {

        [EmailAddress] 
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FullName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public IFormFile ProfilePicture { get; set; }
        public EducationalDegree EducationalDegree { get; set; }
        public string? JobTitle { get; set; }
        public string? Facebook { get; set; }
        public string? Twitter { get; set; }
        public string? Whatsapp { get; set; }
        public string? Telegram { get; set; }
    }
}
