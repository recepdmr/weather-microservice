using System.Text;
using Domain.Dtos.Jwt;
using Domain.Dtos.Result;
using MediatR;

namespace Application.UseCases.Users.Commands.LoginWithRefreshToken
{
    public class LoginWithRefreshTokenCommand : IRequest<IResult>
    {
        public string RefreshToken { get; set; }

        public byte[] RefreshTokenHash => Encoding.ASCII.GetBytes(RefreshToken);
    }
}