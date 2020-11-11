using System.Text;
using Domain.Dtos.Jwt;
using MediatR;

namespace Application.UseCases.Users.Commands.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommand : IRequest<IJwtResult>
    {
        public string RefreshToken { get; set; }

        public byte[] RefreshTokenHash => Encoding.ASCII.GetBytes(RefreshToken);
    }
}