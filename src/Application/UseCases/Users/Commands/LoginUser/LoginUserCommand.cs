using Core.Abstraction.MediatR;

namespace Core.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommand : IMediatrRequest
    {
        public string EmailAddress { get; set; }
        public string Password { get; set; }
    }
}