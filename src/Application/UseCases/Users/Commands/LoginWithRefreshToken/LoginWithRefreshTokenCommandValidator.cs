using Core.Extensions;
using FluentValidation;

namespace Core.UseCases.Users.Commands.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommandValidator : AbstractValidator<LoginWithRefreshTokenCommand>
    {
        public LoginWithRefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotNull()
                .NotEmpty()
                .Xss()
                ;
        }
    }
}