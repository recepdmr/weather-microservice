using System;

namespace Domain.Dtos.Jwt
{
    public interface IJwtResult
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset TokenExpiresDate { get; set; }
        public DateTimeOffset RefreshTokenExpiresDate { get; set; }
    }
}