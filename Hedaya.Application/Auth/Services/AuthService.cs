using Hedaya.Application.Auth.Abstractions;
using Hedaya.Application.Auth.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Common;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Ocsp;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hedaya.Application.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTSettings _jwt;
        private readonly IEmailSender _sender;
        private IConfiguration _configuration;


        private IApplicationDbContext _context;

        public AuthService(UserManager<AppUser> userManager, IApplicationDbContext context, IOptions<JWTSettings> jwt, RoleManager<IdentityRole> roleManager, IEmailSender sender, IConfiguration configuration)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _sender = sender;
            _context = context;
            _configuration = configuration;
        }

        public async Task<AuthModel> LoginAsync(ModelStateDictionary modelState,TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {

                modelState.AddModelError("Invalid Login", "Email Or Password Is Incorrect");

                return null;
              
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            authModel.Message = "Login Successfully";
            authModel.IsAuthinticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Email = user.Email;
            authModel.UseName = user.UserName;
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;

            var rolesList = await _userManager.GetRolesAsync(user);
            authModel.Roles = rolesList.ToList();


            return authModel;
        }

        public async Task<AuthModel> RegisterAsync(ModelStateDictionary modelState,RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                modelState.AddModelError("userEmail", "Email Is Already Exists!");

                return null;
            }
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
            {
                modelState.AddModelError("userName", "User name Is Already Exists!");

                return null;
               
            }
            string code = RandomHelper.RandonString(5);

            var user = new AppUser
            {
                Name = model.UserName,
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                UserType = Hedaya.Domain.Enums.UserType.User,
                EmailConfirmed = true,
                SecurityCode= code,
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {

                var erros = string.Empty;
                foreach (var error in result.Errors)
                {
                    erros += $"{error.Description},";
                    return new AuthModel { Message = erros };
                }

            }

            await _userManager.AddToRoleAsync(user, "User");
            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthinticated = true,
                Roles = new List<string> { "Users" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                UseName = user.UserName,
                Message = "Registered Successfully!"
            };
        }


        public async Task<string> AddToRoleAsync(ModelStateDictionary modelState, AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
            {
                modelState.AddModelError("Invalid", "Invalid user ID or Role");
                return null;
            }
             
            

            if (await _userManager.IsInRoleAsync(user, model.Role))
            {
                modelState.AddModelError("Invalid", "User already assigned to this role");
                return null;
               
            }
                

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something went wrong";
        }






   


        public async Task<dynamic> ForgetPasswordAsync(ModelStateDictionary modelState, ForgotPasswordVM userModel)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userModel.Email);
                if (user == null)
                {
                    modelState.AddModelError("user_email", "There is no user with that Email Address");
                    return null;
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var encodedToken = Encoding.UTF8.GetBytes(token);
                var validToken = WebEncoders.Base64UrlEncode(encodedToken);
                var activateCode = RandomHelper.RandonString(5);

                ///ToDo Send mail
             

               var message = $"Hi {user.Name}, To Reset Your Password Please Use This Code : {activateCode}";
              await _sender.SendEmailAsync(user.Email, message, activateCode);


                user.SecurityCode = activateCode;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        modelState.AddModelError(error.Code, error.Description);
                        return null;
                    }
                }

                
                return new
                {
                    result = new
                    {
                        code = activateCode
                    }
                };
            }
            catch (Exception ex)
            {
                modelState.AddModelError(string.Join(",", ex.Data), string.Join(",", ex.InnerException));
                return null;
            }
        }



        public async Task<dynamic> RestPasswordAsync(ModelStateDictionary modelState, ResetPasswordModel userModel)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userModel.user_email);
                if (user == null)
                {
                    modelState.AddModelError("user_email", "There is no user with that Email Address");
                    return null;
                }

                if (userModel.user_password != userModel.user_password_confirm)
                {
                    modelState.AddModelError("Password Or Confirm Password", "Confirm password doesn't match the password");
                    return null;
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, userModel.user_password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        modelState.AddModelError(error.Code, error.Description);
                        return null;
                    }
                }
                return new
                {
                    result = new { }
                };

            }
            catch (Exception ex)
            {
                modelState.AddModelError(string.Join(",", ex.Data), string.Join(",", ex.InnerException));
                return null;
            }
        }


        public async Task<dynamic> GetUserAsync(ModelStateDictionary modelState, string token)
        {
            try
            {
                string userIdFromToken = JWTHelper.GetPrincipal(token, _configuration);
                var user = await _userManager.FindByIdAsync(userIdFromToken);
                if (user == null)
                {
                    modelState.AddModelError("Access Token", "There is no user with that Access token");
                    return null;
                }


             
                return new
                {
                    result = new
                    {
                        user = new
                        {
                            user_id = user.Id,
                            user_name = user.UserName,
                            user_email = user.Email,
                            user_phone = user.PhoneNumber,
                   
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                modelState.AddModelError(string.Join(",", ex.Data), string.Join(",", ex.InnerException));
                return null;
            }
        }


        public async Task<dynamic> UpdateUserAsync(ModelStateDictionary modelState, UpdateProfileModel userModel, string token)
        {
            try
            {
                string userIdFromToken = JWTHelper.GetPrincipal(token, _configuration);
                var user = await _userManager.FindByIdAsync(userIdFromToken);
                if (user == null)
                {
                    modelState.AddModelError("Access Token", "There is no user with that Access token");
                    return null;
                }


                var phoneisexist = _context.AppUsers.FirstOrDefault(s => s.PhoneNumber == userModel.userPhone && s.Id != user.Id);
                if (phoneisexist != null)
                {
                    modelState.AddModelError("user_Phone", "Phone Number Is Already Exist");
                    return null;
                }
            

               
                if (!string.IsNullOrEmpty(userModel.userName))
                {
                    user.Name = userModel.userName;
                }
                if (!string.IsNullOrEmpty(userModel.userEmail))
                {
                    user.Email = userModel.userEmail;
                }
                if (!string.IsNullOrEmpty(userModel.userPhone))
                {
                    user.PhoneNumber = userModel.userPhone;
                }

                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        modelState.AddModelError(error.Code, error.Description);
                        return null;
                    }
                }



                return new
                {
                    result = new
                    {
                        activate = user.EmailConfirmed,
                        access_token = token,
                        user = new
                        {
                            userId = user.Id,
                            userName = user.Name,
                            userEmail = user.Email,
                            userPhone = user.PhoneNumber,                                                
                        }
                    },
                    msg = "Successfully Message"
                };
            }
            catch (Exception ex)
            {
                modelState.AddModelError(string.Join(",", ex.Data), string.Join(",", ex.InnerException));
                return null;
            }

        }


        public async Task<dynamic> DeleteAccount(ModelStateDictionary modelState, string token)
        {
            try
            {
                string userIdFromToken = JWTHelper.GetPrincipal(token, _configuration);
                var user = await _userManager.FindByIdAsync(userIdFromToken);
                if (user == null)
                {
                    modelState.AddModelError("Access Token", "There is no user with that Access token");
                    return null;
                }

                user.Deleted = true;
                _context.SaveChanges();

                return new
                {
                    result = new
                    {

                    },
                    msg = "DeletedSuccessfully"
                };
            }
            catch (Exception ex)
            {
                modelState.AddModelError(string.Join(",", ex.Data), string.Join(",", ex.InnerException));
                return null;
            }
        }






        private async Task<JwtSecurityToken> CreateJwtToken(AppUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id)
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(_jwt.DurationInDays),
                signingCredentials: signingCredentials);

            return jwtSecurityToken;
        }

  


    }
}
