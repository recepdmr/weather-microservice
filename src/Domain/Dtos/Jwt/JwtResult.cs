using System;

namespace Domain.Dtos.Jwt
{
    public class JwtResult : IJwtResult
    {
        public JwtResult(string jwtToken, string refreshToken, DateTimeOffset tokenExpiresDate,
            DateTimeOffset refreshTokenExpiresDate)
        {
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
            TokenExpiresDate = tokenExpiresDate;
            RefreshTokenExpiresDate = refreshTokenExpiresDate;
        }

        public DateTimeOffset TokenExpiresDate { get; set; }
        public DateTimeOffset RefreshTokenExpiresDate { get; set; }

        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
    }
}