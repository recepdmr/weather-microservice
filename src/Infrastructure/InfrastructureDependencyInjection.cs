﻿using Infrastructure.Weathers;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastucture(this IServiceCollection services)
        {
            services.AddSingleton<IWeatherService, NullWeatherService>();
        }
    }
}