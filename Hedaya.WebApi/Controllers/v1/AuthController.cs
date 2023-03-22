using API.Errors;
using FluentValidation;
using Hedaya.Application.Auth.Abstractions;
using Hedaya.Application.Auth.Models;
using Hedaya.Application.Auth.Validators;
using Hedaya.Application.Complexes.Queries;
using Hedaya.Domain.Entities;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace Hedaya.WebApi.Controllers.v1
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]

    public class AuthController : BaseController<AuthController>
    {
        private readonly IAuthService _authService;
        private readonly UserManager<AppUser> _userManager;
        public AuthController(IAuthService authService, UserManager<AppUser> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }


        [HttpPost("SetLanguage")]
        public async Task<IActionResult> SetLanguage(string? culture)
        {

            if (string.IsNullOrEmpty(culture) || (culture != "en-US" && culture != "ar-EG"))
            {

                return BadRequest(new { Message = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"عفوا لقد حدث خطأ" : "Something Went wrong" });
            }

            Response.Cookies.Append(

                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) }
                );
            var currentCulture = new { Message = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"لغة التطبيق الحالية هي اللغة العربية" : "The current application language is English." };

            return Ok(currentCulture);
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {

            var validator = new RegisterModelValidator(_userManager);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(error => new { error = error.ErrorMessage })
                    .ToList();

                return BadRequest(new { errors });
            }




            var result = await _authService.RegisterAsync(ModelState, model);


            return Ok(result);
        }




        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] TokenRequestModel model)
        {
            var validator = new TokenRequestModelValidator(_userManager);
            var validationResult = await validator.ValidateAsync(model);

            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors
                    .Select(error => new { error = error.ErrorMessage })
                    .ToList();

                return BadRequest(new { errors });
            }




            var result = await _authService.LoginAsync(ModelState, model);



            return Ok(result);
        }


        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordVM userModel)
        {



            var validationResult = await new ForgotPasswordVMValidator(_userManager).ValidateAsync(userModel);

            if (!validationResult.IsValid)
            {

                var errors = validationResult.Errors
                     .Select(error => new { error = error.ErrorMessage })
                     .ToList();

                return BadRequest(new { errors });
            }


            var result = await _authService.ForgetPasswordAsync(ModelState, userModel);

            return Ok(result);



        }

        [HttpPost("ResetPassowrd")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordModel userModel)
        {
            try
            {
                var validationResult = await new ResetPasswordModelValidator(_userManager).ValidateAsync(userModel);
                if (!validationResult.IsValid)
                {

                    var errors = validationResult.Errors
                         .Select(error => new { error = error.ErrorMessage })
                         .ToList();

                    return BadRequest(new { errors });
                }

                var result = await _authService.RestPasswordAsync(ModelState, userModel);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return CustomBadRequest.CustomExErrorResponse(ex);
            }
        }


        [HttpGet("Profile")]
        public async Task<IActionResult> ProfileUser()
        {
            try
            {

                // Get the current user ID from the JWT token
                var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;

                var result = await _authService.GetUserAsync(ModelState, userId);
                if (!ModelState.IsValid)
                {
                    return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
                }
                if (result == null)
                {
                    return Unauthorized();
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return CustomBadRequest.CustomExErrorResponse(ex);
            }
        }


        [HttpPut("UpdateProfile")]
        public async Task<ActionResult> UpdateProfile([FromBody] UpdateProfileModel userModel)
        {

            var validationResult = await new UpdateProfileModelValidator(_userManager).ValidateAsync(userModel);
            if (!validationResult.IsValid)
            {

                var errors = validationResult.Errors
                     .Select(error => new { error = error.ErrorMessage })
                     .ToList();

                return BadRequest(new { errors });
            }


            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;

                var result = await _authService.UpdateUserAsync(userModel, userId);
            if(result == null)
                return BadRequest(new { error = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? "عفوا لقد حدث خطأ" : "Something Went Wrong!" });

            return Ok(result);
           
        }

        [HttpPut("UpdateProfilePicture")]
        public async Task<ActionResult> UpdateProfilePicture([FromForm] UpdateProfilePictureModel UpdateProfilePictureModel)
        {
            var validationResult = await new UpdateProfilePictureModelValidator(_userManager).ValidateAsync(UpdateProfilePictureModel);
            if (!validationResult.IsValid)
            {

                var errors = validationResult.Errors
                     .Select(error => new { error = error.ErrorMessage })
                     .ToList();

                return BadRequest(new { errors });
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;

            var result = await _authService.UpdateProfilePicture(UpdateProfilePictureModel, userId);
            if (result == null)
                return BadRequest(new { error = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? "عفوا لقد حدث خطأ" : "Something Went Wrong!" });
            return Ok(result);


        }


        [HttpPost("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            var result = await _authService.ChangePasswordAsync(userId, model.CurrentPassword, model.NewPassword, ModelState);
            if (result is null)
            {
                return BadRequest(new { error = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? "عفوا لقد حدث خطأ" : "Something Went Wrong!" });
            }

            return Ok(result);
        }

        //[HttpPost("addToRole")]
        //public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        //{
        //    if (!ModelState.IsValid)
        //        return CustomBadRequest.CustomModelStateErrorResponse(ModelState);

        //    var result = await _authService.AddToRoleAsync(ModelState, model);

        //    if (!string.IsNullOrEmpty(result))
        //        return CustomBadRequest.CustomModelStateErrorResponse(ModelState);


        //    return Ok(model);
        //}

        [HttpDelete("DeleteAccount")]
        public async Task<ActionResult> DeleteAccount(string reason)
        {

            var userId = User.Claims.FirstOrDefault(c => c.Type == "uid")?.Value;
            var result =await _authService.DeleteAccount(reason, userId);
            if (result is null)
            {
                return BadRequest(new { error = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? $"عفوا لايوجد مستخدم بهذا المعرف {userId}" : $"The User  With This Id {userId} Is Not Found" });
            }
            return Ok(result);
        }
        [HttpGet("TermsAndConditions")]
        public async Task<IActionResult> TermsAndConditions()
        {

            return Ok(await Mediator.Send(new GetTermsAndConditionQuery()));
        }


    }
}
