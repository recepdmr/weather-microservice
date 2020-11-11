using System;
using Domain.Dtos.Result;

namespace Domain.Dtos.Jwt
{
    public interface IJwtResult  : IResult
    {
        public string JwtToken { get; set; }
        public string RefreshToken { get; set; }
        public DateTimeOffset TokenExpiresDate { get; set; }
        public DateTimeOffset RefreshTokenExpiresDate { get; set; }
    }
}