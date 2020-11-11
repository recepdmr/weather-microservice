using Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class ApplicationDependencyInjection
    {
        public static void AddApplication(this IServiceCollection services)
        {
            services.AddInfrastucture();

            services.AddMediatR(typeof(ApplicationDependencyInjection));
        }
    }
}