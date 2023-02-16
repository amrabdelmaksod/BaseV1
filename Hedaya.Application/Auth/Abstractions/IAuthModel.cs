﻿using Hedaya.Application.Auth.Models;
using Hedaya.Common;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Hedaya.Application.Auth.Abstractions
{
    public interface IAuthService
    {
        Task<AuthModel> RegisterAsync(RegisterModel model);
        Task<AuthModel> LoginAsync(TokenRequestModel model);
        Task<dynamic> ForgetPasswordAsync(ModelStateDictionary modelState, ForgotPasswordVM userModel);
        Task<dynamic> RestPasswordAsync(ModelStateDictionary modelState, ResetPasswordModel userModel);
        Task<dynamic> GetUserAsync(string Id);
        Task<dynamic> UpdateUserAsync(string Id, UpdateProfileModel userModel);
        Task<string> AddToRoleAsync(AddRoleModel model);
        Task<dynamic> DeleteAccount(string Id);


        //object LogOut(ModelStateDictionary modelState, string authorization, string MobilID);

    }
}