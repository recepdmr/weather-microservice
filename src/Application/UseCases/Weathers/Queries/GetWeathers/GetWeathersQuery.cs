using System.Collections.Generic;
using Domain.Weathers;
using MediatR;

namespace Application.UseCases.Weathers.Queries.GetWeathers
{
    public class GetWeathersQuery : IRequest<IReadOnlyCollection<WeatherForecast>>
    {
    }
}