using Hedaya.Application.Auth.Abstractions;
using Hedaya.Application.Auth.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Common;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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

        public AuthService(UserManager<AppUser> userManager, IOptions<JWTSettings> jwt, RoleManager<IdentityRole> roleManager, IEmailSender sender, IConfiguration configuration,IApplicationDbContext context)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _sender = sender;
            _configuration = configuration;
            _context = context;
        }

        public async Task<AuthModel> LoginAsync(TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email Or Password Is Incorrect";
                return authModel;
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

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                return new AuthModel { Message = "Email Is Already Exists!" };
            }
            if (await _userManager.FindByNameAsync(model.UserName) is not null)
            {
                return new AuthModel { Message = "User name Is Already Exists!" };
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


        public async Task<string> AddToRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid user ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something went wrong";
        }






        //public async Task<Response> ForgotPasswordAsync(string Email)
        //{
        //    if (await _userManager.FindByEmailAsync(Email) is null)
        //    {
        //        return new Response { Message = "User Not Found!", Result=new { } };
        //    }
        //  // Send Forgot Password Email
        //  var appUser = await _userManager.FindByEmailAsync(Email);
        //  var code = await _userManager.GeneratePasswordResetTokenAsync(appUser);
        //    var callBackUrl = $"https://localhost/api/v1/Auth?id={appUser.Id}&code=${code}";
        //    var subject = $"Hi {appUser.Name} Please Click On This Link To Reset Password";
        //  code = HttpUtility.UrlEncode(code);

        //    await _sender.SendEmailAsync(appUser.Email, subject, callBackUrl);

        //    return new Response { Message = "Email Sent Successfully", Result= new {appUser.Id } };

        //}


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


        public async Task<dynamic> GetUserAsync(string Id)
        {
            
                var user = await _userManager.FindByIdAsync(Id);
                
                if (user == null)
                {
                  return new Response { Message ="User Not Found", Result = new {Id = Id} };
                }                

                return new
                {
                    result = new
                    {
                        user = new
                        {
                            userId = user.Id,
                            userName = user.Name,
                            userEmail = user.Email,
                            userPhone = user.PhoneNumber,                                               
                        }
                    }
                };                      
        }




        public async Task<dynamic> UpdateUserAsync(string Id, UpdateProfileModel userModel)
        {
           
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return new Response { Message = "User Not Found", Result = new { Id = Id } };
                }

                var phoneisexist = _context.AppUsers.FirstOrDefault(s => s.PhoneNumber == userModel.userPhone && s.Id != user.Id);
                if (phoneisexist != null)
                {
                    return new Response { Message = "Phone Is Already Exists", Result = new { Id = Id } };
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
               await _context.SaveChangesAsync();
            if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        return new Response { Message = "Something Went Wrong!", Result = new { Id = Id } };
                    }
                }

               

                return new
                {
                    result = new
                    {
                        activate = user.EmailConfirmed,
                       
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


        public async Task<dynamic> DeleteAccount(string Id)
        {
            try
            {
               
                var user = await _userManager.FindByIdAsync(Id);
                if (user == null)
                {
                    return new Response { Message = "User Not Found!", Result = new { Id = Id } };
                }

                user.Deleted = true;
              await _context.SaveChangesAsync();    
       

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

                throw new Exception(ex.Message);
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
