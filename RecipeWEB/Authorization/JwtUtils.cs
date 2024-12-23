using Microsoft.Extensions.Options;
using RecipeWEB.Helpers;
using RecipeWEB.Authorization;
using RecipeWEB.Models;
using RecipeWEB.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Org.BouncyCastle.Crypto;
using System;
using System.Security.Cryptography;
using Microsoft.EntityFrameworkCore;

namespace RecipeWEB.Authorization
{
    public class JwtUtils : IJwtUtils
    {
        private readonly AppSettings _appSettings;
        private readonly RecipeContext _context;
        
        public JwtUtils(RecipeContext context, IOptions<AppSettings> appSettings)
        {
            _context = context;
            _appSettings = appSettings.Value;
        }

        public string GenerateJwtToken(User account)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[] { new Claim("id", account.UserId.ToString())}),
                Expires = DateTime.UtcNow.AddMinutes(15),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescription);
            return tokenHandler.WriteToken(token);
        }

        public async Task<RefreshToken> GenerateRefreshToken(string ipAddress)
        {
            var refreshToken = new RefreshToken
            {
                Token = Convert.ToHexString(RandomNumberGenerator.GetBytes(64)),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreateByIp = ipAddress
            };
            var tokenIsUnique = (await _context.Users.AnyAsync(a => a.RefreshTokens.Any(t => t.Token == refreshToken.Token)));
            if (tokenIsUnique)
                return await GenerateRefreshToken(ipAddress);
            return refreshToken;
        }

        public int? ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);
                var jwtToken = (JwtSecurityToken)validatedToken;
                var accountId = int.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);

                return accountId;
            }
            catch
            {
                return null;
            }
        }
    }
}