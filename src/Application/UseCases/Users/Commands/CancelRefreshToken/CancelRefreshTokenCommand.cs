using System.Text;
using Domain.Dtos.Result;
using MediatR;

namespace Application.UseCases.Users.Commands.CancelRefreshToken
{
    public class CancelRefreshTokenCommand : IRequest<Result>
    {
        public string RefreshToken { get; set; }

        public byte[] RefreshTokenHash =>
            !string.IsNullOrEmpty(RefreshToken) ? Encoding.UTF8.GetBytes(RefreshToken) : default;
    }
}