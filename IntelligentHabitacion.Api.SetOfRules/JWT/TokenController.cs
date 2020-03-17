using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace IntelligentHabitacion.Api.SetOfRules.JWT
{
    public class TokenController
    {
        private const string Email = "eml";

        public string CreateToken(string email)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var claims = new List<Claim>
            {
                new Claim(Email, email)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Expires = DateTime.UtcNow.AddMinutes(180), // 3 hours
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(SimetricKey(), SecurityAlgorithms.HmacSha256Signature)
            };

            var securityToken = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(securityToken);
        }

        public string User(string token)
        {
            var conteudoToken = ValidateToken(token);
            return conteudoToken.FindFirst(Email).Value;
        }

        private ClaimsPrincipal ValidateToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            var validationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                ValidateIssuer = false,
                ValidateAudience = false,
                IssuerSigningKey = SimetricKey(),
                ClockSkew = new TimeSpan(0)
            };
            var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
            return principal;
        }

        private SymmetricSecurityKey SimetricKey()
        {
            var signingKey = "aT5mOmpufG5SaStlKiozKG1iOkhnPiZBfGo8aDV0RCp9JSM1VWhZMHhKXmVbek5lW0xtWUhTWkczWG9hKnNIIVkraHRTNUsmJkdMNzVhRHlscCx0OU0xZWpHNFJ6QH07eWFpKDx0eiQ=";
            var symmetricKey = Convert.FromBase64String(signingKey);
            return new SymmetricSecurityKey(symmetricKey);
        }
    }
}
