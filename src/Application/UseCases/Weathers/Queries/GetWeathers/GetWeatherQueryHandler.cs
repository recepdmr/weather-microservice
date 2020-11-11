using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Domain.Weathers;
using Infrastructure.Weathers;
using MediatR;

namespace Application.UseCases.Weathers.Queries.GetWeathers
{
    public class GetWeatherQueryHandler : IRequestHandler<GetWeathersQuery, IReadOnlyCollection<WeatherForecast>>
    {
        private readonly IWeatherService _weatherService;

        public GetWeatherQueryHandler(IWeatherService weatherService)
        {
            _weatherService = weatherService;
        }

        public Task<IReadOnlyCollection<WeatherForecast>> Handle(GetWeathersQuery request,
            CancellationToken cancellationToken)
        {
            //TODO : adding caching provider
            return _weatherService.GetWeatherForecastsAsync(cancellationToken);
        }
    }
}