using System;
using System.Threading;
using Application;
using Infrastructure.DataAccess.EntityFrameworkCore.SeedData;
using Infrastructure.Jwt;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using StackExchange.Profiling.SqlFormatters;

namespace Api
{
    public class Startup
    {
        private readonly IConfiguration _configuration;

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var jwtOptions = _configuration.GetSection(nameof(JwtOptions)).Get<JwtOptions>();

            services.Configure<JwtOptions>(_configuration.GetSection(nameof(JwtOptions)));

            services.AddMemoryCache();

            services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler";
                options.SqlFormatter = new InlineFormatter();

                options.TrackConnectionOpenClose = true;
            }).AddEntityFramework();

            services.AddApplication(jwtOptions, GetDefaultConnectionString());

            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Weather Restfull Api",
                    TermsOfService = new Uri("https://recepdmr.com/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Recep Demir",
                        Email = "recepdemir3438@gmail.com",
                        Url = new Uri("http://recepdmr.com/")
                    }
                });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });
        }

        private string GetDefaultConnectionString()
        {
            return _configuration.GetConnectionString("Default");
        }

        public async void Configure(IApplicationBuilder app, IWebHostEnvironment env,
            UserSeedDataProvider userSeedDataProvider)
        {
            app.UseMiniProfiler();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();


            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            await userSeedDataProvider.SeedDataAsync(CancellationToken.None);
        }
    }
}