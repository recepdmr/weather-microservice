using FluentValidation;

namespace Application.UseCases.Users.Commands.CancelRefreshToken
{
    public class CancelRefreshTokenCommandValidator : AbstractValidator<CancelRefreshTokenCommand>
    {
        public CancelRefreshTokenCommandValidator()
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