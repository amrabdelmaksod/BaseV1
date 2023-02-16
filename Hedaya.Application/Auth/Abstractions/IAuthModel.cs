using Hedaya.Application.Auth.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hedaya.Application.Auth.Abstractions
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(TokenRequestModel model);
        Task<dynamic> ForgetPasswordAsync(ModelStateDictionary modelState, ForgotPasswordVM userModel);
        Task<dynamic> RestPasswordAsync(ModelStateDictionary modelState, ResetPasswordModel userModel);
        Task<dynamic> GetUserAsync(ModelStateDictionary modelState, string token);
        Task<dynamic> UpdateUserAsync(ModelStateDictionary modelState, UpdateProfileModel userModel, string token);
     
        Task<string> AddToRoleAsync(AddRoleModel model);
        Task<dynamic> DeleteAccount(ModelStateDictionary modelState, string token);


        //object LogOut(ModelStateDictionary modelState, string authorization, string MobilID);

    }
}
