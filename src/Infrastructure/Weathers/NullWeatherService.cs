using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Domain.Weathers;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace Infrastructure.Weathers
{
    public class NullWeatherService : IWeatherService
    {
        private static readonly string[] Summaries =
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<NullWeatherService> _logger;

        public NullWeatherService(ILogger<NullWeatherService> logger)
        {
            _logger = logger ?? NullLogger<NullWeatherService>.Instance;
        }

        public async Task<IReadOnlyCollection<WeatherForecast>> GetWeatherForecastsAsync(
            CancellationToken cancellationToken)
        {
            _logger.LogDebug("{Service} used", nameof(NullWeatherService));

            var rng = new Random();
            return await Task.FromResult(
                Enumerable.Range(1, 5).Select(index =>
                        new WeatherForecast(DateTime.Now.AddDays(index),
                            rng.Next(-20, 55),
                            Summaries[rng.Next(Summaries.Length)]
                        ))
                    .ToList());
        }
    }
}