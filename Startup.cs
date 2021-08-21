using BankingServices.StartupConfigurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

namespace BankingServices
{
	/// <summary>
	/// Startup class for BankingServices.
	/// </summary>
	public class Startup
	{
		/// <summary>
		/// Instantiates Startup class.
		/// </summary>
		/// <param name="configuration">instance of configuration manage.r</param>
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		/// <summary>
		/// Gets the instance of <see cref="IConfiguration"/>
		/// </summary>
		public IConfiguration Configuration { get; }

		/// <summary>
		/// Gets or sets the logger instance.
		/// </summary>
		public ILogger Logger { get; set; }

		/// <summary>
		/// This method gets called by the runtime. Use this method to add services to the container.
		/// </summary>
		/// <param name="services">collection of servies.</param>
		public void ConfigureServices(IServiceCollection services)
		{
			services.ConfigureDependencies();
			services.ConfigureAuthenticationService(Configuration);

			services.AddControllers(options =>
			{
				options.ReturnHttpNotAcceptable = true;

			}).AddXmlDataContractSerializerFormatters();

			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "BankingServices", Version = "v1" });

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});
		}

		/// <summary>
		/// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		/// </summary>
		/// <param name="app">application builder.</param>
		/// <param name="env">web host environment.</param>
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BankingServices v1"));
			}

			app.UseHttpsRedirection();

			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
