using BankingServices.MiddlewareComponents;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;

namespace BankingServices
{
	/// <summary>
	/// Class to configure app's request pipeline.
	/// </summary>
	public class StartupFilter : IStartupFilter
	{
		/// <summary>
		/// Configures app's request pipeline.
		/// </summary>
		/// <param name="next">next middleware to execute.</param>
		/// <returns>application builder.</returns>
		public Action<IApplicationBuilder> Configure(Action<IApplicationBuilder> next)
		{
			return builder =>
			{
				// TODO: Add middleware builder.UseMiddleware<>();
				builder.UseMiddleware<RequestLoggerMiddleware>();
				next(builder);
			};
		}
	}
}
