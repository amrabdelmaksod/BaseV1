using System.ComponentModel.DataAnnotations;

namespace Hedaya.Application.Auth.Models
{
    public class ChangePasswordModel
    {
        [Required(ErrorMessage = "The current password is required.")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }

        [Required(ErrorMessage = "The new password is required.")]
        [DataType(DataType.Password)]
        [RegularExpression("^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\\w\\s]).{8,}$",
            ErrorMessage = "The password must be at least 8 characters long and contain at least one uppercase letter, one lowercase letter, one number, and one special character.")]
        public string NewPassword { get; set; }

        [Required(ErrorMessage = "The new password confirmation is required.")]
        [DataType(DataType.Password)]
        [Compare(nameof(NewPassword), ErrorMessage = "The new password and confirmation password do not match.")]
        public string ConfirmNewPassword { get; set; }
    }

}
