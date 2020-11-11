using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddInfrastructure();

            services.AddMediatR(typeof(ApplicationDependencyInjection));
        }
    }
}