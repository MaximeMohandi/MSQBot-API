using Microsoft.EntityFrameworkCore;
using MSQBot_API.Interfaces;
using MSQBot_API.Services.ImageScrapper;
using MSQBot_API.Services.MovieServices;

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
                    .WithMethods("GET", "POST")
                    .WithHeaders("Origin", "X-Requested-With", "Content-Type", "Accept"));
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
        /// <param name="config"></param>
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
            services.AddScoped<MovieServices, MovieServices>();
            services.AddScoped<IImageScrapperService, GoogleImageScrapperServices>();
        }
    }
}