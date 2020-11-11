using Domain.Dtos.Result;
using MediatR;

namespace Application.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommand : IRequest<IResult>
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}