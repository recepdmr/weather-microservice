using System.Linq;
using AutoMapper;
using Infrastructure;
using Infrastructure.Jwt;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplication(this IServiceCollection services, JwtOptions jwtOptions,
            string connectionString)
        {
            services.AddInfrastructure(jwtOptions, connectionString);

            services.AddMediatR(typeof(ApplicationDependencyInjection));

            services.AddAutoMapper();
        }

        /// <summary>
        ///     Populate Assembly all AutoMapper Profile
        /// </summary>
        /// <param name="services"></param>
        private static void AddAutoMapper(this IServiceCollection services)
        {
            var profiles = typeof(ApplicationDependencyInjection).Assembly.GetTypes()
                .Where(x => x.BaseType == typeof(Profile)).ToList();

            var configuration = new MapperConfiguration(cfg =>
            {
                foreach (var profile in profiles) cfg.AddProfile(profile);
            });

            var mapper = new Mapper(configuration);

            services.AddSingleton<IMapper>(mapper);
        }
    }
}