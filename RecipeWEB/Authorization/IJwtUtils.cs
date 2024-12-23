using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Org.BouncyCastle.Crypto;
using RecipeWEB.Entities;
using RecipeWEB.Helpers;
using RecipeWEB.Models;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace RecipeWEB.Authorization
{

    public interface IJwtUtils
    {
        public string GenerateJwtToken(User account);
        public int? ValidateJwtToken(string token);
        public Task<RefreshToken> GenerateRefreshToken(string ipAddress);
    }
}
