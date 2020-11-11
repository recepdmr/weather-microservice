using Domain.Dtos.Jwt;
using MediatR;

namespace Application.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<IJwtResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}