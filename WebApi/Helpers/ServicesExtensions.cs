using BusinessLogicLayer.Patterns.Structural.Implementations.Facades;
using BusinessLogicLayer.Patterns.Structural.Interfaces.Facades;
using BusinessLogicLayer.Services.Implementations;
using BusinessLogicLayer.Services.Interfaces;
using BusinessLogicLayer.Utilities;
using DataAccessLayer.Repositories.Implementations;
using DataAccessLayer.Repositories.Interfaces;
using DataAccessLayer.UnitsOfWork.Implementations;
using DataAccessLayer.UnitsOfWork.Interfaces;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Models.Entities;
using Newtonsoft.Json;
using WebApi.Filters.Exception;

namespace WebApi.Helpers
{
    public static class ServicesExtensions
    {
        public static void InjectDependencies(this IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IResultRepository, ResultRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IGameService, GameService>();
            services.AddScoped<IResultService, ResultService>();
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddTransient<IUserAndGameVerificator, UserAndGameVerificator>();
            services.AddTransient<IImageUploader, CloudinaryImageUploader>();
        }

        public static void ConfigureAuthorization(this IServiceCollection services)
        {
            services.AddAuthorization(options =>
            {
                options.AddPolicy("AuthenticatedOnly", policy => policy.RequireAuthenticatedUser());
                options.AddPolicy("AdministratorsOnly", policy => policy.RequireClaim("role", "admin"));
            });
        }

        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder => builder.AllowAnyOrigin()
                                                                  .AllowAnyMethod()
                                                                  .AllowAnyHeader()
                                                                  .AllowCredentials());
            });
        }

        public static void ConfigureMvc(this IServiceCollection services)
        {
            services.AddMvc(options => options.Filters.Add<AsyncExceptionFilter>())
                    .AddFluentValidation(options => options.RegisterValidatorsFromAssemblyContaining<Startup>())
                    .AddJsonOptions(options => options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore)
                    .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public static void ConfigureAuthentication(this IServiceCollection services)
        {
            //TODO: Figure out how to inject necessary settings here
        }
    }
}