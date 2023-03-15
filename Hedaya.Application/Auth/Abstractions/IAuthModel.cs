using Hedaya.Application.Auth.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hedaya.Application.Auth.Abstractions
{
    public interface IAuthService
    {
        Task<dynamic> RegisterAsync(ModelStateDictionary modelState, RegisterModel model);
        Task<dynamic> LoginAsync(ModelStateDictionary modelState, TokenRequestModel model);
        Task<dynamic> ForgetPasswordAsync(ModelStateDictionary modelState, ForgotPasswordVM userModel);
        Task<dynamic> RestPasswordAsync(ModelStateDictionary modelState, ResetPasswordModel userModel);
        Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword, ModelStateDictionary modelState);
        Task<dynamic> GetUserAsync(ModelStateDictionary modelState, string userId);
        Task<dynamic> UpdateUserAsync(ModelStateDictionary modelState, UpdateProfileModel userModel, string userId);
     
        //Task<string> AddToRoleAsync(ModelStateDictionary modelState, AddRoleModel model);
        Task<dynamic> DeleteAccount(ModelStateDictionary modelState, string token);


        //object LogOut(ModelStateDictionary modelState, string authorization, string MobilID);

    }
}
