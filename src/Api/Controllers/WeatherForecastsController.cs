using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Api.Abstractions;
using Application.UseCases.Weathers.Queries.GetWeathers;
using Domain.Weathers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Controllers
{
    public class WeatherForecastsController : WeatherMicroserviceControllerBase
    {
        private readonly ILogger<WeatherForecastsController> _logger;

        public WeatherForecastsController(IMediator mediator,
            ILogger<WeatherForecastsController> logger) : base(mediator)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet]
        [Authorize]
        public async Task<IReadOnlyCollection<WeatherForecast>> GetAsync()
        {
            _logger.LogDebug("{ControllerName}", nameof(WeatherForecastsController));
            var result = await Mediator.Send(new GetWeathersQuery());

            return result;
        }
    }
}