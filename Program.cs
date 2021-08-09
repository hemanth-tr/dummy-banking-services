using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace BankingServices
{
	/// <summary>
	/// Main program.
	/// </summary>
	public class Program
	{
		/// <summary>
		/// Starting point of method.
		/// </summary>
		public static void Main()
		{
			var webHost = CreateHostBuilder();
			var initialzedHost = webHost.Build();
			initialzedHost.Run();
		}

		/// <summary>
		/// Creates host.
		/// </summary>
		/// <returns>host.</returns>
		public static IHostBuilder CreateHostBuilder()
		{
			var defaultBuilder = Host.CreateDefaultBuilder();

			var webHost = defaultBuilder.ConfigureWebHostDefaults(webBuilder =>
			{
				webBuilder.UseStartup<Startup>();
			});

			return webHost;
		}
	}
}
