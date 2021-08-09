using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace BankingServices.MiddlewareComponents
{
	/// <summary>
	/// Middleware which logs request details.
	/// </summary>
	public class RequestLoggerMiddleware
	{
		private readonly RequestDelegate _next;

		/// <summary>
		/// Instantiates <see cref="RequestLoggerMiddleware"/>
		/// </summary>
		/// <param name="next">delegate which executes next middleware.</param>
		/// <param name="logger">type of ILogger.</param>
		public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
		{
			_next = next;
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
		}

		/// <summary>
		/// Gets or sets the instance of logger.
		/// </summary>
		public ILogger<RequestLoggerMiddleware> Logger { get; set; }

		/// <summary>
		/// Invoked by host.
		/// </summary>
		/// <param name="context">current http context.</param>
		/// <returns>task.</returns>
		public async Task Invoke(HttpContext context)
		{
			if (context.Request.Method == "POST")
			{
				Logger.LogInformation($"IP:{context.Connection.RemoteIpAddress},\nPath:{context.Request.Path},\nActionVerb:{context.Request.Method}");
			}
			await _next(context);
		}
	}
}
