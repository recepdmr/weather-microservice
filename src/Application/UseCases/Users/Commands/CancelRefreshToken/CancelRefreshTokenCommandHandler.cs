using System;
using System.Threading;
using System.Threading.Tasks;
using Domain.Dtos.Result;
using Domain.Users;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Application.UseCases.Users.Commands.CancelRefreshToken
{
    public class CancelRefreshTokenCommandHandler : IRequestHandler<CancelRefreshTokenCommand, Result>
    {
        private readonly UserManager<User> _userManager;

        public CancelRefreshTokenCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<Result> Handle(CancelRefreshTokenCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.RefreshTokenHash == request.RefreshTokenHash,
                cancellationToken);

            if (user == null)
                return new Result("not found user");

            user.RefreshTokenHash = null;
            user.RefreshTokenExpiresDate = DateTimeOffset.MinValue;

           await _userManager.UpdateAsync(user);
           
           return new Result("Token revoked");
        }
    }
}