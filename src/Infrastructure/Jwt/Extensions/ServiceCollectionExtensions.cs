using System;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Core.Services.Jwt.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJwtAuthentication(this IServiceCollection services, JwtOptions options)
        {
            services.AddAuthentication(authenticationOptions =>
                    {
                        authenticationOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        authenticationOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    }
                )
                .AddJwtBearer(opt =>
                {
                    opt.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateAudience = true,
                        ValidAudience = options.Audience,
                        ValidateIssuer = true,
                        ValidIssuer = options.Issuer,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(options.SecurityKey))
                    };

                    opt.Events = new JwtBearerEvents
                    {
                        OnTokenValidated = ctx => Task.CompletedTask,
                        OnAuthenticationFailed = ctx =>
                        {
                            Console.WriteLine("Exception:{0}", ctx.Exception.Message);
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}