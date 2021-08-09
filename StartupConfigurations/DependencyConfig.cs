using BankingServices.Repository;
using BankingServices.Repository.SqlRepository;
using Microsoft.Extensions.DependencyInjection;

namespace BankingServices.StartupConfigurations
{
    /// <summary>
    /// External class to handle application dependencies.
    /// </summary>
    public static class DependencyConfig
    {
        /// <summary>
        /// Method for configuring application dependencies.
        /// </summary>
        /// <param name="serviceCollection">service collection.</param>
        public static void ConfigureDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped<IBankRepository, BankRepository>();
        }
    }
}
