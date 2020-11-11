using System.Text;
using Core.Abstraction.MediatR;

namespace Core.UseCases.Users.Commands.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommand : IMediatrRequest
    {

        public string RefreshToken { get; }

        public byte[] RefreshTokenHash => Encoding.ASCII.GetBytes(RefreshToken);
    }
}