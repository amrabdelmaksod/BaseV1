using Hedaya.Application.Auth.Abstractions;
using Hedaya.Application.Auth.Models;
using Hedaya.Application.Helpers;
using Hedaya.Application.Interfaces;
using Hedaya.Common;
using Hedaya.Domain.Entities;
using Hedaya.Domain.Entities.Authintication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crmf;
using System.Globalization;
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
     
        private readonly ISMSService _smsService;
        public static IHostingEnvironment _environment;


        private IApplicationDbContext _context;
        private readonly SignInManager<AppUser> _signInManager;

        public AuthService(UserManager<AppUser> userManager,
            IApplicationDbContext context, 
            IOptions<JWTSettings> jwt,
            RoleManager<IdentityRole> roleManager,
            IEmailSender sender,
            ISMSService smsService, 
            SignInManager<AppUser> signInManager,IHostingEnvironment environment)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _sender = sender;
            _context = context;
            _smsService = smsService;
            _signInManager = signInManager;
            _environment = environment; 
        }



        const string accountSid = "AC506a6a755cc66accb4d4913600aed0ee";
        const string authToken = "d26d77a6670a369ec76cb91650dd3f8d";

        public async Task<dynamic> RegisterAsync(ModelStateDictionary modelState, RegisterModel model)
        {
                     
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
                var errors = result.Errors.Select(e => new { error = e.Description }).ToList();
                return new { errors };
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

            var authModel = new AuthModel
            {
                Email = user.Email,
                FullName = TraineeUser.FullName,
                ExpiresOn = jwtSecurityToken.ValidTo,
                IsAuthinticated = true,
                Roles = new List<string> { "Users" },
                Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken),
                Mobile = user.PhoneNumber,
                Message = "Registered Successfully!"
            };

            return new { Result = authModel };
               
        
        }

        public async Task<dynamic> LoginAsync(ModelStateDictionary modelState, TokenRequestModel model, string WebRootPath)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByNameAsync(model.Mobile);
         
          

            var trainee = await _context.Trainees.FirstOrDefaultAsync(a => a.AppUserId == user.Id);


            string ProfilePicture = null;
            if (trainee.ProfilePictureImagePath != null)
            {
                ProfilePicture = Path.Combine(WebRootPath, "ImagePath", trainee.ProfilePictureImagePath ?? "1");

            }

            var jwtSecurityToken = await CreateJwtToken(user);
            authModel.Message = "Login Successfully";
            authModel.Email = user.Email;
            authModel.ProfilePicture = ProfilePicture;
            authModel.IsAuthinticated = true;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            authModel.Mobile = user.UserName; // assuming the mobile number is stored in the UserName property
            authModel.ExpiresOn = jwtSecurityToken.ValidTo;
            authModel.FullName = trainee.FullName;

            var rolesList = await _userManager.GetRolesAsync(user);
            authModel.Roles = rolesList.ToList();

            return new {Result = authModel } ;
        }



        #region AddToRole
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
        #endregion

        public async Task<dynamic> ForgetPasswordAsync(ModelStateDictionary modelState, ForgotPasswordVM userModel)
        {
            try
            {

                var user = await _userManager.FindByNameAsync(userModel.MobileNumber);
             
                // Generate a random 6-digit verification code
                var verificationCode = new Random().Next(1000, 9999).ToString();

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
                    var errors = result.Errors.Select(e => new { error = e.Description }).ToList();
                    return new { errors };
                }

                return new
                {
                    Result = new
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
                var user = await _userManager.FindByNameAsync(userModel.Mobile);
                if (user == null)
                {
                  
                    var errors = new List<object> { new { error = "There is no user with that Email Address" } };
                    return new { errors };
                }

                if (userModel.Password != userModel.ConfirmPassword)
                {
                   
                    var errors = new List<object> { new { error = "Confirm password doesn't match the password" } };
                    return new { errors };
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                var result = await _userManager.ResetPasswordAsync(user, token, userModel.Password);
                if (!result.Succeeded)
                {
                    var errors = result.Errors.Select(e => new { error = e.Description }).ToList();
                    return new { errors };
                }
                return new
                {
                    result = new { Message = "Password Changed Successfully" }
                };

            }
            catch (Exception ex)
            {
              
                return new { errors = $"{ex.Message}" };
            }
        }


        public async Task<dynamic> GetUserAsync(ModelStateDictionary modelState, string userId, string WebRootPath)
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

                string ProfilePicture = null;
                if (trainee.ProfilePictureImagePath != null)
                {
                    ProfilePicture = Path.Combine(WebRootPath, "ImagePath", trainee.ProfilePictureImagePath ?? "1");

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
                            ProfilePicture = ProfilePicture,
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



        public async Task<dynamic> UpdateUserAsync( UpdateProfileModel userModel, string userId)
        {
            try
            {

                if(userId == null) { return null; }

                var user = await _userManager.FindByIdAsync(userId);
                var Trainee = await _context.Trainees.FirstAsync(a => a.AppUserId == userId);
                if (user is null || Trainee is null)
                {                  
                    return null;
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

                if (!string.IsNullOrEmpty(userModel.Country.ToString()))
                {
                    user.Nationality = userModel.Country;
                }

                if (!string.IsNullOrEmpty(userModel.Gender.ToString()))
                {
                    user.Gender = userModel.Gender;
                }

                if (userModel.DateOfBirth != null)
                {
                    user.DateOfBirth = userModel.DateOfBirth;
                }

                _context.Trainees.Update(Trainee);
                await _context.SaveChangesAsync();



                var result = await _userManager.UpdateAsync(user);
              
                    if (!result.Succeeded)
                    {
                        var errors = result.Errors.Select(e => new { error = e.Description }).ToList();
                        return new { errors };
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
             
                return new { Message = $"{ex.Message}" };
            }

        }


        public async Task<object> ChangePasswordAsync(string userId, string currentPassword, string newPassword, ModelStateDictionary modelState)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
               return null;
            }

            var result = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => new { error = e.Description }).ToList();
                return new { errors };
            }

            await _signInManager.RefreshSignInAsync(user);

            return new {Message =CultureInfo.CurrentCulture.TwoLetterISOLanguageName =="ar" ? "تم تغيير كلمة السر بنجاح"  : "Password Changed Successfully"};
        }


        public async Task<dynamic> DeleteAccount(string Reason, string userId)
        {
            try
            {
              
                    var user = await _context.AppUsers.FirstOrDefaultAsync(a=>a.Id == userId);
                var trainee = await _context.Trainees.FirstOrDefaultAsync(a=>a.AppUserId == userId);
                if (user == null )
                {                  
                    return null;
                }

                if(trainee is not null)
                {
                    trainee.Deleted = true;
                }

                user.Deleted = true;
                user.DeletedReason = Reason;
                await _context.SaveChangesAsync();

                return new
                {

                    Message = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? "تم ازالة الحساب بنجاح" : "Account Deleted Successfully"
                };
            }
            catch (Exception ex)
            {
             
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

        public async Task<object> UpdateProfilePicture(UpdateProfilePictureModel userModel, string userId, string WebRootPath)
        {
            var Trainee = await _context.Trainees.FirstOrDefaultAsync(a => a.AppUserId == userId);

            if (userModel.ProfilePicture != null && Trainee is not null)
            {
                var imagePath = Path.Combine(WebRootPath, "ImagePath");
                Trainee.ProfilePictureImagePath =await ImageHelper.SaveImage(userModel.ProfilePicture, _environment);
                await _context.SaveChangesAsync();
                var Message = CultureInfo.CurrentCulture.TwoLetterISOLanguageName == "ar" ? "تم تحديث صورة الملف الشخصي بنجاح" : "Profile Picture Updated Successfully";
                return new { result = Message };
            }

            return null;
        }
    }
}
