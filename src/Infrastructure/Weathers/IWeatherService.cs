using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Weathers;

namespace Infrastructure.Weathers
{
    public interface IWeatherService
    {
        Task<IReadOnlyCollection<WeatherForecast>> GetWeatherForecastsAsync(
            CancellationToken cancellationToken);
    }
}