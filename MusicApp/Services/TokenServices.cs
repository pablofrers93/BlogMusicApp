﻿using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MusicApp.Services
{
    public class TokenServices : ITokenServices
    {
        private readonly IConfiguration _configuration;
         public TokenServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateToken(string userEmail)
        {


            var claims = new[]
            {
                new Claim(ClaimTypes.Email, userEmail)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("JWT:key").Value));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: credentials
                );

            var token = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return token;
        }
    }
}
