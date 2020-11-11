using Core.Entities.Users;
using Core.Extensions;
using FluentValidation;

namespace Core.UseCases.Users.Commands.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommand>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.EmailAddress)
                .NotNull()
                .NotEmpty()
                .EmailAddress()
                .Xss()
                .Length(UserConst.MinEmailAddressLength, UserConst.MaxEmailAddressLength);
        }
    }
}