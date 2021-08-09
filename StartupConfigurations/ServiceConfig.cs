using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BankingServices.StartupConfigurations
{
    /// <summary>
    /// External class to configure startup services.
    /// </summary>
    public static class ServiceConfig
    {
        /// <summary>
        /// Method to configure services.
        /// </summary>
        /// <param name="serviceCollection">service collection.</param>
        /// <param name="configuration">configuration to read application settings.</param>
        public static void ConfigureAuthenticationService(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddAuthentication((options) =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer((options) =>
            {
                options.Authority = $"https://{configuration["Auth0:Domain"]}/";
                options.Audience = configuration["Auth0:Audience"];
            });
        }
    }
}
