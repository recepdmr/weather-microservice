using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation;
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
            services.AddScoped(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.AddFluentValidationValidators();
            services.AddAutoMapper();
        }

        private static void AddFluentValidationValidators(this IServiceCollection services)
        {
            var validationTypes = (from x in typeof(ServiceCollectionExtensions).Assembly.GetTypes()
                from z in x.GetInterfaces()
                let y = x.BaseType
                where
                    y != null && y.IsGenericType &&
                    typeof(IValidator<>).IsAssignableFrom(y.GetGenericTypeDefinition()) ||
                    z.IsGenericType &&
                    typeof(IValidator<>).IsAssignableFrom(z.GetGenericTypeDefinition())
                select x).ToList();


            foreach (var validationType in validationTypes)
            {
                if (validationType.BaseType == null) continue;
                var entity = validationType.BaseType.GetGenericArguments().First();

                var baseValidation = typeof(IValidator<>).MakeGenericType(entity);

                services.AddTransient(baseValidation, validationType);
            }
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

    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest
    {
        private readonly IValidator<TRequest> _validator;

        public ValidationBehavior(IValidator<TRequest> validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            await _validator.ValidateAndThrowAsync(request, cancellationToken);
            return await next();
        }
    }
}