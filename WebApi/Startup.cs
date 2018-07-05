using System;
using System.Text;
using AutoMapper;
using BusinessLogicLayer.Helpers;
using DataAccessLayer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using WebApi.Helpers;

namespace WebApi
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration) => Configuration = configuration;

        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<AppDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddDbContext<AppDbContext>(options => options.UseLazyLoadingProxies().UseInMemoryDatabase());
            services.Configure<JwtSettings>(Configuration.GetSection("JWT"));
            services.InjectDependencies();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                    .AddJwtBearer(options =>
                     {
                         options.SaveToken            = true;
                         options.IncludeErrorDetails  = true;
                         options.RequireHttpsMetadata = true;

                         options.TokenValidationParameters = new TokenValidationParameters
                         {
                             ValidIssuer              = Configuration["JWT:Issuer"],
                             ValidAudience            = Configuration["JWT:Audience"],
                             ValidateIssuer           = true,
                             ValidateAudience         = true,
                             ValidateLifetime         = true,
                             RequireExpirationTime    = true,
                             ValidateIssuerSigningKey = true,
                             ClockSkew                = TimeSpan.FromMinutes(5),
                             IssuerSigningKey         = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["JWT:Key"]))
                         };
                     });
            services.ConfigureAuthorization();
            services.ConfigureCors();
            services.AddAutoMapper();
            services.ConfigureMvc();
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
            app.UseCors("CorsPolicy");
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}