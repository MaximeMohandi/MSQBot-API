using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MSQBot_API.Business.Services;
using MSQBot_API.Business.Services.ImageScrapper;
using MSQBot_API.Core.Entities;
using MSQBot_API.Core.Repositories;
using MSQBot_API.Infrastructure;
using MSQBot_API.Infrastructure.Data.Repositories;
using MSQBot_API.Interfaces;

namespace MSQBot_API.Extensions
{
    public static class ServiceExtensions
    {
        /// <summary>
        /// CORS (Cross-Origin Resource Sharing) is a mechanism that gives rights to the user to access resources from the server on a different domain
        /// Used if the client is on a different domain than the API
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            });
        }

        /// <summary>
        /// Used to assist IIS deployement
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureIISIntegration(this IServiceCollection services)
        {
            // default configuration
            services.Configure<IISOptions>(options =>
            {
            });
        }

        /// <summary>
        /// Configure connection to the Mysql server
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public static void ConfigureMySqlContext(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MSQBotDb");
            services.AddDbContext<MSQBotDbContext>(o => o.UseMySql(connectionString, MySqlServerVersion.LatestSupportedServerVersion));
        }

        /// <summary>
        /// Configure a repository wrapper
        /// </summary>
        /// <param name="services"></param>
        public static void ConfigureBusinessServices(this IServiceCollection services)
        {
            //services
            services.AddScoped<MovieServices, MovieServices>();
            services.AddScoped<RateServices, RateServices>();
            services.AddScoped<IUserAuthenticationServices, AuthenticationServices>();
            services.AddScoped<IImageScrapperService, GoogleImageScrapperServices>();

            //repositories
            services.AddScoped<IMovieRepository, MovieRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IRateRepository, RateRepository>();
        }

        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //add identity system
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MSQBotDbContext>().AddDefaultTokenProviders();

            //add jwt settings
            var bindJwtSettings = new JwtConfiguration();
            configuration.Bind("JWT", bindJwtSettings);
            services.AddSingleton(bindJwtSettings);

            //configure authentication
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(bindJwtSettings.IssuerSigningKey)),
                    ValidateIssuer = bindJwtSettings.ValidateIssuer,
                    ValidIssuer = bindJwtSettings.ValidIssuer,
                    ValidateAudience = bindJwtSettings.ValidateAudience,
                    ValidAudience = bindJwtSettings.ValidAudience,
                    RequireExpirationTime = bindJwtSettings.RequireExpirationTime,
                    ValidateLifetime = bindJwtSettings.RequireExpirationTime,
                    ClockSkew = TimeSpan.Zero,
                };

                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                        {
                            context.Response.Headers.Add("IS-TOKEN-EXPIRED", "true");
                        }
                        return Task.CompletedTask;
                    }
                };
            });
        }
    }
}