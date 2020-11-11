using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Abstractions
{
    [ApiController]
    [Route("[controller]")]
    public abstract class WeatherMicroserviceControllerBase : ControllerBase
    {
        protected WeatherMicroserviceControllerBase(IMediator mediator)
        {
            Mediator = mediator;
        }

        protected IMediator Mediator { get; }
    }
}