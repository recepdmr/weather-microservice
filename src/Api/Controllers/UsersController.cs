using System.Threading;
using System.Threading.Tasks;
using Api.Abstractions;
using Application.UseCases.Users.Commands.CancelRefreshToken;
using Application.UseCases.Users.Commands.LoginUser;
using Application.UseCases.Users.Commands.LoginWithRefreshToken;
using Domain.Dtos.Jwt;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    public class UsersController : WeatherMicroserviceControllerBase
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        /// <summary>
        ///     Login application
        /// </summary>
        /// <param name="loginUserCommand">command</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        [HttpPost("authenticate")]
        [ProducesResponseType(typeof(JwtResult), StatusCodes.Status201Created)]
        public async Task<IActionResult> LoginAsync([FromBody] LoginUserCommand loginUserCommand,
            CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(loginUserCommand, cancellationToken));
        }

        /// <summary>
        ///     Login with refresh token.
        /// </summary>
        /// <param name="loginWithRefreshTokenCommand">command</param>
        /// <param name="cancellationToken">cancellation token</param>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        [ProducesResponseType(typeof(JwtResult), StatusCodes.Status201Created)]
        public async Task<IActionResult> LoginWithRefreshTokenAsync(
            [FromBody] LoginWithRefreshTokenCommand loginWithRefreshTokenCommand,
            CancellationToken cancellationToken = default)
        {
            return Ok(await Mediator.Send(loginWithRefreshTokenCommand, cancellationToken));
        }

        /// <summary>
        ///     cancell refresh token
        /// </summary>
        /// <param name="cancelRefreshTokenCommand"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        [HttpPost("revoke-refresh-token")]
        public async Task<IActionResult> RevokeRefreshToken(
            [FromBody] CancelRefreshTokenCommand cancelRefreshTokenCommand, CancellationToken cancellationToken)
        {
            return Ok(await Mediator.Send(cancelRefreshTokenCommand, cancellationToken));
        }
    }
}