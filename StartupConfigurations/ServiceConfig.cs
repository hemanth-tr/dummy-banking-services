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
        public static IServiceCollection ConfigureAuthenticationService(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            return serviceCollection;
        }
    }
}
