using FluentValidation;

namespace Application.UseCases.Users.Commands.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommandValidator : AbstractValidator<LoginWithRefreshTokenCommand>
    {
        public LoginWithRefreshTokenCommandValidator()
        {
            RuleFor(x => x.RefreshToken)
                .NotNull()
                .NotEmpty()
                .Must(CheckXss)
                ;
        }

        private bool CheckXss(string arg)
        {
            return arg.Contains("<");
        }
    }
}