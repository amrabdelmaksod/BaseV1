  using API.Errors;
using Hedaya.Application.Auth.Abstractions;
using Hedaya.Application.Auth.Models;
using Hedaya.Application.Complexes.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Hedaya.WebApi.Controllers.v1
{
  
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController :BaseController<AuthController>
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }


        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
            }
            

            var result =await _authService.RegisterAsync(ModelState, model);
            if(!result.IsAuthinticated)
            {
                return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
            }
           
            return Ok(result);
        }


        
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody]  TokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
            }


            var result = await _authService.LoginAsync(ModelState, model);
            if (!result.IsAuthinticated)
            {
                return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
            }

            return Ok(result);
        }


        [HttpPost("forgotPassword")]
        public async Task<IActionResult> ForgotPasswordAsync([FromBody] ForgotPasswordVM userModel)
        {
            var result = await _authService.ForgetPasswordAsync(ModelState, userModel);
            if (!ModelState.IsValid)
            {
                return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
            }
            return Ok(result);

         

        }

        [HttpPost("ResetPassowrd")]
        public async Task<IActionResult> RestPassword([FromBody] ResetPasswordModel userModel)
        {
            try
            {
                var result = await _authService.RestPasswordAsync(ModelState, userModel);
                if (!ModelState.IsValid)
                {
                    return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
                }
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
                var headers = Request.Headers;
                if (!headers.ContainsKey("token"))
                {
                    ModelState.AddModelError("token", "Missing tokin");
                    if (!ModelState.IsValid)
                    {
                        return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
                    }
                }
                if (!headers.ContainsKey("lang"))
                {
                    ModelState.AddModelError("lang", "Missing lang");
                    if (!ModelState.IsValid)
                    {
                        return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
                    }
                }
                string lang = headers.GetCommaSeparatedValues("lang").First();
               
                string token = headers.GetCommaSeparatedValues("token").First();

                var result = await _authService.GetUserAsync(ModelState, token);
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
        public async Task<ActionResult> UpdateProfile([FromForm] UpdateProfileModel userModel)
        {
            try
            {
                var headers = Request.Headers;
                if (!headers.ContainsKey("token"))
                {
                    ModelState.AddModelError("token", "Missing tokin");
                    if (!ModelState.IsValid)
                    {
                        return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
                    }
                }

                string token = headers.GetCommaSeparatedValues("token").First();
                var result = await _authService.UpdateUserAsync(ModelState, userModel, token);
                if (!ModelState.IsValid)
                {
                    return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return CustomBadRequest.CustomExErrorResponse(ex);
            }
        }

        [HttpPost("addToRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return CustomBadRequest.CustomModelStateErrorResponse(ModelState);

            var result = await _authService.AddToRoleAsync(ModelState, model);

            if (!string.IsNullOrEmpty(result))
                return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
        

            return Ok(model);
        }

        [HttpDelete("DeleteAccount")]
        public async Task<ActionResult> DeleteAccount()
        {
            try
            {

                var headers = Request.Headers;



                if (!headers.ContainsKey("token"))
                {
                    ModelState.AddModelError("token", "Missing tokin");
                    if (!ModelState.IsValid)
                    {
                        return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
                    }
                }
                var token = headers.FirstOrDefault(s => s.Key == "token").Value;

                var result = _authService.DeleteAccount(ModelState, token);

                if (!ModelState.IsValid)
                {
                    return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                return CustomBadRequest.CustomExErrorResponse(ex);
            }
        }



        [HttpGet("TermsAndConditions")]
        public async Task<IActionResult> TermsAndConditions()
        {

            return Ok(await Mediator.Send(new GetComplexDataQuery()));
        }


    }
}
