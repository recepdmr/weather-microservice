using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Abstraction.MediatR;
using Core.Entities.Users;
using Core.Services.Jwt;
using Core.Utilities.ResultManagement;
using Microsoft.AspNetCore.Identity;

namespace Core.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : MediatrRequestHandler<LoginUserCommand>
    {
        private readonly IJwtService _jwtService;
        private readonly UserManager<User> _userManager;

        public LoginUserCommandHandler(
            UserManager<User> applicationUserManager,
            IJwtService jwtService)
        {
            _jwtService = Check.NotNull(jwtService);
            _userManager = Check.NotNull(applicationUserManager);
        }

        protected override async Task<IResult> HandleAsync(LoginUserCommand request,
            CancellationToken cancellationToken = default)
        {
            var user = await _userManager.FindByEmailAsync(request.EmailAddress);

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result) throw new ApplicationException(MessageConst.NotFoundUser);

            var claims = await _userManager.GetClaimsAsync(user);

            await AddClaimRoleAsync(claims, user);

            var jwtResult = _jwtService.CreateJwtResult(claims);

            user.RefreshTokenHash = Encoding.ASCII.GetBytes(jwtResult.RefreshToken);
            user.RefreshTokenExpiresDate = jwtResult.RefreshTokenExpiresDate;
            await _userManager.UpdateAsync(user);

            return jwtResult;
        }

        private async Task AddClaimRoleAsync(ICollection<Claim> claims, User user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var rolesString = string.Join(',', roles);
            claims.Add(new Claim(ClaimTypes.Role, rolesString));
        }
    }
}