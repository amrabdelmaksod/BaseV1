using Hedaya.Application.Auth.Abstractions;
using Hedaya.Application.Auth.Models;
using Hedaya.Application.Interfaces;
using Hedaya.Common;
using Hedaya.Domain.Entities;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace Hedaya.Application.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly JWTSettings _jwt;
        private readonly IEmailSender _sender;
        private IConfiguration _configuration;
        private readonly ISMSService _smsService;


        private IApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(UserManager<AppUser> userManager, IApplicationDbContext context, IOptions<JWTSettings> jwt, RoleManager<IdentityRole> roleManager, IEmailSender sender, IConfiguration configuration, ISMSService smsService, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _sender = sender;
            _context = context;
            _configuration = configuration;
            _smsService = smsService;
            _signInManager = signInManager;
        }



        const string accountSid = "AC506a6a755cc66accb4d4913600aed0ee";
        const string authToken = "d26d77a6670a369ec76cb91650dd3f8d";

        public async Task<dynamic> RegisterAsync(ModelStateDictionary modelState, RegisterModel model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
            {
                modelState.AddModelError("userEmail", "Email Is Already Exists!");

                return new { Message = $"There user with this Email is already exists!  {model.Email}" };


            }

            string code = RandomHelper.RandomString(5);

            var user = new AppUser
            {

                UserName = model.PhoneNumber,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Nationality = model.Country,
                UserType = Domain.Enums.UserType.User,
                EmailConfirmed = true,
                SecurityCode = code,
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

            var TraineeUser = new Trainee
            {
                Id = user.Id,
                FullName = model.FullName,
                AppUserId = user.Id,
                CreatedById = "2b07b902-5701-439d-b157-1bcfdf9a3466",
                CreationDate = DateTime.Now,
            };

            await _context.Trainees.AddAsync(TraineeUser);
            await _context.SaveChangesAsync();

            await _userManager.AddToRoleAsync(user, "User");
            var jwtSecurityToken = await CreateJwtToken(user);
            return new AuthModel
            {
                Email = user.Email,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthinticated = true,
                Roles = new List<string> { "Users" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Mobile = user.PhoneNumber,
                Message = "Registered Successfully!"
            };
        }

        public async Task<AuthModel> LoginAsync(ModelStateDictionary modelState, TokenRequestModel model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByNameAsync(model.Mobile);
            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                modelState.AddModelError("Invalid Login", "Mobile number or password is incorrect.");
                authModel.Message = "Invalid Login";
                authModel.IsAuthinticated = false; // Set IsAuthenticated to false
                return authModel;
            }

            var jwtSecurityToken = await CreateJwtToken(user);
            authModel.Message = "Login Successfully";
            authModel.Email = user.Email;
            authModel.IsAuthinticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Mobile = user.UserName; // assuming the mobile number is stored in the UserName property
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;

            var rolesList = await _userManager.GetRolesAsync(user);
            authModel.Roles = rolesList.ToList();

            return authModel;
        }





        //public async Task<string> AddToRoleAsync(ModelStateDictionary modelState, AddRoleModel model)
        //{
        //    var user = await _userManager.FindByIdAsync(model.UserId);

        //    if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
        //    {
        //        modelState.AddModelError("Invalid", "Invalid user ID or Role");
        //        return null;
        //    }



        //    if (await _userManager.IsInRoleAsync(user, model.Role))
        //    {
        //        modelState.AddModelError("Invalid", "User already assigned to this role");
        //        return null;

        //    }


        //    var result = await _userManager.AddToRoleAsync(user, model.Role);

        //    return result.Succeeded ? string.Empty : "Something went wrong";
        //}









        public async Task<dynamic> ForgetPasswordAsync(ModelStateDictionary modelState, ForgotPasswordVM userModel)
        {
            try
            {

                var user = await _userManager.FindByNameAsync(userModel.MobileNumber);
                // Generate a random 6-digit verification code
                var verificationCode = new Random().Next(100000, 999999).ToString();

                // Send the verification code to the user's phone number via SMS

                //TwilioClient.Init(accountSid, authToken);
                //var message = MessageResource.Create(
                //    body: $"Hi {user.UserName}, To Reset Your Password Please Use This Code: {verificationCode}",
                //    from: new PhoneNumber("your_twilio_phone_number_here"),
                //    to: new PhoneNumber(user.PhoneNumber)
                //);

                // Update the user's SecurityCode with the verification code
                user.SecurityCode = verificationCode;
                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        modelState.AddModelError(error.Code, error.Description);
                        return new { Message = $"{error.Description}" };
                    }
                }

                return new
                {
                    result = new
                    {
                        code = verificationCode
                    }
                };

            }
            catch (Exception ex)
            {
                modelState.AddModelError(string.Join(",", ex.Data), string.Join(",", ex.InnerException));
                return new { Message = $"{ex.Message}" };
            }
        }

        public async Task<bool> VerifyUserByMobileNumberAsync(string mobileNumber, string verificationCode)
        {
            // Find the user by mobile number
            var user = await _userManager.FindByNameAsync(mobileNumber);
            if (user == null)
            {
                return false; // User not found
            }

            // Verify the user's phone number with the verification code

            if (user.SecurityCode != verificationCode)
            {
                return false;
            }

            // Update the user's phone number confirmation status
            user.PhoneNumberConfirmed = true;
            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return false; // Failed to update user
            }

            return true; // User verified successfully
        }




        public async Task<dynamic> RestPasswordAsync(ModelStateDictionary modelState, ResetPasswordModel userModel)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userModel.user_email);
                if (user == null)
                {
                    modelState.AddModelError("user_email", "There is no user with that Email Address");
                    return new { Message = "There is no user with that Email Address" };
                }

                if (userModel.user_password != userModel.user_password_confirm)
                {
                    modelState.AddModelError("Password Or Confirm Password", "Confirm password doesn't match the password");
                    return new { Message = "Confirm password doesn't match the password" };
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, userModel.user_password);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        modelState.AddModelError(error.Code, error.Description);
                        return new { Message = $"{error.Code}, {error.Description}" };
                    }
                }
                return new
                {
                    result = new { Message = "Password Changed Successfully" }
                };

            }
            catch (Exception ex)
            {
                modelState.AddModelError(string.Join(",", ex.Data), string.Join(",", ex.InnerException));
                return new { Message = $"{ex.Message}" };
            }
        }


        public async Task<dynamic> GetUserAsync(ModelStateDictionary modelState, string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);

                var trainee = await _context.Trainees.FirstAsync(a => a.AppUserId == userId);
                if (user == null)
                {
                    modelState.AddModelError("Access Token", $"There is no user with this Id : {userId}");
                    return new { Message = $"There is no user with this Id : {userId}" };
                }

                string profilePictureBase64 = null;
                if (trainee.ProfilePicture != null && trainee.ProfilePicture.Length > 0)
                {
                    profilePictureBase64 = Convert.ToBase64String(trainee.ProfilePicture);
                }

                return new
                {
                    result = new
                    {
                        user = new
                        {
                            Id = user.Id,
                            Email = user.Email,
                            Phone = user.PhoneNumber,
                            FullName = trainee.FullName,
                            DateOfBirth = user.DateOfBirth,
                            Country = user.Nationality,
                            Gender = user.Gender,
                            EducationalDegree = trainee.EducationalDegree,
                            JobTitle = trainee.JobTitle ?? "",
                            Facebook = trainee.Facebook ?? "",
                            Twitter = trainee.Twitter ?? "",
                            Whatsapp = trainee.Whatsapp ?? "",
                            Telegram = trainee.Telegram ?? "",
                            profilePictureBase64 = profilePictureBase64,
                        }
                    }
                };
            }
            catch (Exception ex)
            {
                modelState.AddModelError(string.Join(",", ex.Data), string.Join(",", ex.InnerException));
                return new { Message = $"{ex.Message}" };
            }
        }



        public async Task<dynamic> UpdateUserAsync(ModelStateDictionary modelState, UpdateProfileModel userModel, string userId)
        {
            try
            {

                var user = await _userManager.FindByIdAsync(userId);
                var Trainee = await _context.Trainees.FirstAsync(a => a.AppUserId == userId);
                if (user == null)
                {
                    modelState.AddModelError("Access Token", $"There is no user with this Id : {userId}");
                    return new { Message = $"There is no user with this Id : {userId}" };
                }


                var phoneisexist = _context.AppUsers.FirstOrDefault(s => s.PhoneNumber == userModel.Phone && s.Id != user.Id);
                if (phoneisexist != null)
                {
                    modelState.AddModelError("user_Phone", "Phone Number Is Already Exist");
                    return new { Message = $"Phone Number Is Already Exist : {userModel.Phone}" };
                }




                if (!string.IsNullOrEmpty(userModel.Email))
                {
                    user.Email = userModel.Email;
                }
                if (!string.IsNullOrEmpty(userModel.Phone))
                {
                    user.PhoneNumber = userModel.Phone;
                    user.UserName = userModel.Phone;
                }

                if (!string.IsNullOrEmpty(userModel.FullName))
                {
                    Trainee.FullName = userModel.FullName;
                }

                if (!string.IsNullOrEmpty(userModel.Twitter))
                {
                    Trainee.Twitter = userModel.Twitter;
                }

                if (!string.IsNullOrEmpty(userModel.Facebook))
                {
                    Trainee.Facebook = userModel.Facebook;
                }
                if (!string.IsNullOrEmpty(userModel.JobTitle))
                {
                    Trainee.JobTitle = userModel.JobTitle;
                }
                if (!string.IsNullOrEmpty(userModel.Whatsapp))
                {
                    Trainee.Whatsapp = userModel.Whatsapp;
                }
                if (!string.IsNullOrEmpty(userModel.Telegram))
                {
                    Trainee.Telegram = userModel.Telegram;
                }
                if (!string.IsNullOrEmpty(userModel.EducationalDegree.ToString()))
                {
                    Trainee.EducationalDegree = userModel.EducationalDegree;
                }

                if (!string.IsNullOrEmpty(userModel.ProfilePicture.ToString()))
                {
                    // Save the new profile picture to the database
                    using (var ms = new MemoryStream())
                    {
                        userModel.ProfilePicture.CopyTo(ms);
                        Trainee.ProfilePicture = ms.ToArray();
                    }

                }

                _context.Trainees.Update(Trainee);
                await _context.SaveChangesAsync();



                var result = await _userManager.UpdateAsync(user);
                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        modelState.AddModelError(error.Code, error.Description);
                        return new { Message = $"Something Went Wrong!" };
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
                return new { Message = $"{ex.Message}" };
            }

        }


        public async Task<bool> ChangePasswordAsync(string userId, string currentPassword, string newPassword, ModelStateDictionary modelState)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                modelState.AddModelError("UserId", "User not found");
                return false;
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                {
                    modelState.AddModelError(error.Code, error.Description);
                }
                return false;
            }

            await _signInManager.RefreshSignInAsync(user);

            return true;
        }


        public async Task<dynamic> DeleteAccount(ModelStateDictionary modelState, string userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    modelState.AddModelError("Access Token", $"There is no user with this Id : {userId}");
                    return new { Message = $"There is no user with this Id : {userId}" };
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
                modelState.AddModelError(string.Join(",", ex.Data), string.Join(",", ex.InnerException));
                return new { Message = $"{ex.Message}" };
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


        private static bool IsUsernameTaken(string username, IApplicationDbContext context)
        {
            return context.AppUsers.Any(u => u.UserName == username);
        }

        public static string GenerateUsername(string fullName, IApplicationDbContext context)
        {
            // Remove any leading or trailing white spaces from the full name
            fullName = fullName.Trim();

            // Split the full name into parts using space as the delimiter
            var nameParts = fullName.Split(' ');

            // Combine the first name and last name (if available) to form the username
            var username = nameParts[0];
            if (nameParts.Length > 1)
            {
                username += nameParts[nameParts.Length - 1];
            }

            // If the generated username already exists, append a number to it
            var suffix = 1;
            while (IsUsernameTaken(username, context))
            {
                username = $"{nameParts[0]}{nameParts[nameParts.Length - 1]}{suffix}";
                suffix++;
            }

            return username;
        }


    }
}
