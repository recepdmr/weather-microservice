using System.Collections.Generic;
using System.Security.Claims;
using Domain.Dtos.Jwt;

namespace Infrastructure.Jwt
{
    public interface IJwtService
    {
        IJwtResult CreateJwtResult(IList<Claim> claims);
    }
}