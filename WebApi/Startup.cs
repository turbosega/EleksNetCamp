using System;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApi.Data;
using WebApi.Helpers;
using WebApi.Models.Entities;
using WebApi.Repositories.Implementations;
using WebApi.Repositories.Interfaces;
using WebApi.Services.Implementations;
using WebApi.Services.Interfaces;
using WebApi.UnitsOfWork.Implementations;
using WebApi.UnitsOfWork.Interfaces;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseInMemoryDatabase());
            services.Configure<JwtSettings>(Configuration.GetSection("JWT"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                     {
                         options.SaveToken            = true;
                         options.IncludeErrorDetails  = true;
                         options.RequireHttpsMetadata = true;

                         var jwtSection = Configuration.GetSection("JWT");

                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidIssuer              = jwtSection["Issuer"],
                             ValidAudience            = jwtSection["Audience"],
                             ValidateIssuer           = true,
                             ValidateAudience         = true,
                             ValidateLifetime         = true,
                             ValidateIssuerSigningKey = true,
                             ClockSkew                = TimeSpan.FromMinutes(5),
                             IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]))
                         };
                     });
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddTransient<IPasswordHasher<User>, PasswordHasher<User>>();
            services.AddAutoMapper();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}