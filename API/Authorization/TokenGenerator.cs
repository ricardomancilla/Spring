using Domain.ViewModel;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Configuration;
using System.Security.Claims;
using System.Text;

namespace API.Authorization
{
    public static class TokenGenerator
    {
        public static string GenerateTokenJwt(AuthUserVM user)
        {
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var expireTime = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES"];

            var now = DateTime.UtcNow;

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { 
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Role, user.Role.RoleName)
            });

            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();

            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                subject: claimsIdentity,
                notBefore: now,
                expires: now.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}