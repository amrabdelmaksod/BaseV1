using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Hedaya.Common
{
    public class JWTHelper
    {
        public static string GetPrincipal(string token, IConfiguration _configuration)
        {
            try
            {

                var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration["JWT:Secret"]));

                SecurityToken tokenValidated = null;
                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = signingKey,

                    ValidateAudience = true,
                    ValidAudience = _configuration["JWT:ValidAudience"],

                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JWT:ValidIssuer"],

                    //ValidateLifetime = true


                };
                ClaimsPrincipal principal = handler.ValidateToken(token, validationParameters, out tokenValidated);
                if (principal == null)
                    return null;
                ClaimsIdentity identity = null;
                try
                {
                    identity = (ClaimsIdentity)principal.Identity;
                }
                catch (NullReferenceException)
                {
                    return null;
                }
                Claim userIdClaim = identity.Claims.FirstOrDefault();
                string userId = userIdClaim.Value;
                return userId;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public static string GetToken(string id, IConfiguration _configuration)
        {
            try
            {
                var symmetricKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
                var tokenHandler = new JwtSecurityTokenHandler();
                var now = DateTime.UtcNow;
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                {
                        new Claim("Id",id)
                }),
                    NotBefore = now,
                    Expires = now.AddYears(10),
                    Issuer = _configuration["JWT:ValidIssuer"],
                    Audience = _configuration["JWT:ValidAudience"],
                    IssuedAt = now,
                    SigningCredentials = new SigningCredentials(
                symmetricKey,
                SecurityAlgorithms.HmacSha256Signature),
                };
                var stoken = tokenHandler.CreateToken(tokenDescriptor);
                var token = tokenHandler.WriteToken(stoken);
                return token;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }


        public static string GetUserIdFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var decodedToken = tokenHandler.ReadJwtToken(token);

            // Extract the user ID claim
            string userId = decodedToken.Claims.FirstOrDefault(c => c.Type == "uid").Value;
            return userId;
        }
    }
}
