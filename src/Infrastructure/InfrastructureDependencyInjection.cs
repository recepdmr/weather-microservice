using System;
using Domain.Users;
using Infrastructure.DataAccess.EntityFrameworkCore.DbContexts;
using Infrastructure.DataAccess.EntityFrameworkCore.SeedData;
using Infrastructure.Jwt;
using Infrastructure.Jwt.Extensions;
using Infrastructure.Weathers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class InfrastructureDependencyInjection
    {
        public static void AddInfrastructure(this IServiceCollection services, JwtOptions jwtOptions,
            string connectionString)
        {
            services.AddSingleton<IWeatherService, NullWeatherService>();

            services.AddSingleton<IJwtService, JwtService>();

            services.AddIdentity();
            services.AddJwtAuthentication(jwtOptions);

            services.AddEfCoreWithSqliteAndSeedDataProviders(connectionString);
        }

        private static void AddEfCoreWithSqliteAndSeedDataProviders(this IServiceCollection services,
            string connectionString)
        {
            services.AddDbContextPool<WeatherDbContext>(opt => opt.UseSqlite(connectionString));
            services.AddScoped<UserSeedDataProvider>();
        }

        private static void AddIdentity(this IServiceCollection services)
        {
            services.AddIdentity<User, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<WeatherDbContext>();
        }
    }
}