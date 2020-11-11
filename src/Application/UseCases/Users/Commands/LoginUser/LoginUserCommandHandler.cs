using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Dtos.Result;
using Domain.Users;
using Infrastructure.Jwt;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, IResult>
    {
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public LoginUserCommandHandler(
            UserManager<User> applicationUserManager,
            IJwtService jwtService,
            IMapper mapper)
        {
            _jwtService = jwtService;
            _mapper = mapper;
            _userManager = applicationUserManager;
        }

        public async Task<IResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var mappingUser = _mapper.Map<User>(request);

            var user = await _userManager.FindByNameAsync(mappingUser.UserName);

            var result = await _userManager.CheckPasswordAsync(user, request.Password);

            if (!result) return new Result("Username or password is incorrect");

            var claims = await _userManager.GetClaimsAsync(user);

            var jwtResult = _jwtService.CreateJwtResult(claims);

            user.RefreshTokenHash = Encoding.ASCII.GetBytes(jwtResult.RefreshToken);
            user.RefreshTokenExpiresDate = jwtResult.RefreshTokenExpiresDate;

            await _userManager.UpdateAsync(user);

            return jwtResult;
        }
    }
}