using Domain.Users;
using FluentValidation;

namespace Application.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.Username)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .Must(CheckXss)
                .Length(UserConst.MinUsernameLength, UserConst.MaxUsernameLength);
        }

        private bool CheckXss(string arg)
        {
            return arg.Contains("<");
        }
    }
}