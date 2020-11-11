using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Domain.Dtos.Jwt;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Jwt
{
    public class JwtService : IJwtService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public IJwtResult CreateJwtResult(IList<Claim> claims)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.SecurityKey));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(_jwtOptions.Audience,
                _jwtOptions.Issuer,
                claims,
                expires: DateTime.Now.AddDays(_jwtOptions.ExpireHours),
                signingCredentials: credentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            var refreshToken = CreateRefreshToken();

            var refreshTokenExpiresDate = DateTime.Now.AddHours(_jwtOptions.ExpireHours).AddHours(1);

            return new JwtResult(token, refreshToken,
                DateTime.Now.AddHours(_jwtOptions.ExpireHours),
                refreshTokenExpiresDate);
        }

        private static string CreateRefreshToken()
        {
            var number = new byte[64];
            using var random = RandomNumberGenerator.Create();
            random.GetBytes(number);
            return Convert.ToBase64String(number);
        }
    }
}