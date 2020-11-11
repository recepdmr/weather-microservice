using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Abstraction.MediatR;
using Core.Entities.Users;
using Core.Extensions;
using Core.Services.Jwt;
using Core.Utilities.ResultManagement;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Core.UseCases.Users.Commands.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommandHandler : MediatrRequestHandler<LoginWithRefreshTokenCommand>
    {
        private readonly IJwtService _jwtService;

        private readonly UserManager<User> _userManager;

        public LoginWithRefreshTokenCommandHandler(UserManager<User> userManager, IJwtService jwtService)
        {
            _userManager = Check.NotNull(userManager);
            _jwtService = Check.NotNull(jwtService);
        }

        protected override async Task<IResult> HandleAsync(LoginWithRefreshTokenCommand request,
            CancellationToken cancellationToken = default)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshTokenHash == request.RefreshTokenHash,
                cancellationToken);

            if (user.IsNull() || user.RefreshTokenExpiresDate <= DateTime.Now)
                throw new Exception("Refresh Token is not valid");

            var claims = (await _userManager.GetClaimsAsync(user)).ToList();

            await AddClaimRoleAsync(claims, user);


            var jwtResult = _jwtService.CreateJwtResult(claims);
            user.RefreshTokenHash = Encoding.ASCII.GetBytes(jwtResult.RefreshToken);
            user.RefreshTokenExpiresDate = jwtResult.RefreshTokenExpiresDate;
            await _userManager.UpdateAsync(user);

            return jwtResult;
        }

        private async Task AddClaimRoleAsync(List<Claim> claims, User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var rolesString = string.Join(',', roles);
            claims.Add(new Claim(ClaimTypes.Role, rolesString));
        }
    }
}