using API.Errors;
using BaseV1.Application.Auth.Abstractions;
using BaseV1.Application.Auth.Models;
using Microsoft.AspNetCore.Mvc;

namespace BaseV1.WebApi.Controllers.v1
{
  
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class AuthController :ControllerBase
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
                BadRequest(ModelState);
            }
            

            var result =await _authService.RegisterAsync(model);
            if(!result.IsAuthinticated)
            {
                return BadRequest(result.Message);
            }
           
            return Ok(result);
        }


        
        
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] TokenRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                BadRequest(ModelState);
            }


            var result = await _authService.LoginAsync(model);
            if (!result.IsAuthinticated)
            {
                return BadRequest(result.Message);
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

        [HttpPost("RestPassowrd")]
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
        public async Task<IActionResult> ProfileUser(string Id)
        {
            try
            {
                
                var result = await _authService.GetUserAsync(Id);
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
        public async Task<IActionResult> UpdateProfile(string Id, UpdateProfileModel userModel)
        {
           
              
                if (!ModelState.IsValid)
                {
                    return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
                }

                var result = await _authService.UpdateUserAsync(Id,userModel);
                if (result == null)
                {
                    return Unauthorized();
                }
                return Ok(result);
         
        }

        [HttpPost("addToRole")]
        public async Task<IActionResult> AddRoleAsync([FromBody] AddRoleModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _authService.AddToRoleAsync(model);

            if (!string.IsNullOrEmpty(result))
                return BadRequest(result);

            return Ok(model);
        }

        [HttpDelete("DeleteAccount")]
        public async Task<ActionResult> DeleteAccount(string Id)
        {
            try
            {               
                

                if (!ModelState.IsValid)
                {
                    return CustomBadRequest.CustomModelStateErrorResponse(ModelState);
                }

                var result =await _authService.DeleteAccount(Id);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return CustomBadRequest.CustomExErrorResponse(ex);
            }
        }
    }
}
