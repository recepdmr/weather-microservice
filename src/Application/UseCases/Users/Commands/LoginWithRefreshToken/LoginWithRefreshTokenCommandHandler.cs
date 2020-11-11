using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos.Jwt;
using Domain.Users;
using Infrastructure.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Users.Commands.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommandHandler : IRequestHandler<LoginWithRefreshTokenCommand, IJwtResult>
    {
        private readonly IJwtService _jwtService;

        private readonly UserManager<User> _userManager;

        public LoginWithRefreshTokenCommandHandler(UserManager<User> userManager, IJwtService jwtService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
        }

        public async Task<IJwtResult> Handle(LoginWithRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshTokenHash == request.RefreshTokenHash,
                cancellationToken);

            if (user == null || user.RefreshTokenExpiresDate <= DateTime.Now)
                throw new Exception("Refresh Token is not valid");

            var claims = (await _userManager.GetClaimsAsync(user)).ToList();

            var jwtResult = _jwtService.CreateJwtResult(claims);
            user.RefreshTokenHash = Encoding.ASCII.GetBytes(jwtResult.RefreshToken);
            user.RefreshTokenExpiresDate = jwtResult.RefreshTokenExpiresDate;
            await _userManager.UpdateAsync(user);

            return jwtResult;
        }
    }
}