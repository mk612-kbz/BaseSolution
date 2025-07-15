using AspNetCoreRateLimit;
using CommonApi.Application.Common;
using CommonApi.Extension;
using CommonApi.Middleware;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using Serilog;

namespace CommonApi
{
	public class Program
	{
		public static void Main(string[] args)
		{
			// Configuration files 
			var configuration = new ConfigurationBuilder()
				.SetBasePath(AppContext.BaseDirectory)
				.AddJsonFile($"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json", optional: false, reloadOnChange: true)
				.AddJsonFile("serilog.json", optional: false, reloadOnChange: true)
				.Build();

			// Bind configuration to AppSettings class
			var appSettings = new AppSettings();
			configuration.GetSection("AppSettings").Bind(appSettings);

			var builder = WebApplication.CreateBuilder(args);

			// Configure Serilog
			Log.Logger = new LoggerConfiguration().Enrich.FromLogContext()
				.ReadFrom.Configuration(configuration)
				.Enrich.FromLogContext()
				.CreateLogger();

			// Replace default JSON serialization with Newtosoft
			builder.Services.AddControllers().AddNewtonsoftJson(options =>
			{
				options.SerializerSettings.ContractResolver = new DefaultContractResolver();
				options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
				options.UseMemberCasing();
			});

			// Disable automatic validation error
			builder.Services.Configure<ApiBehaviorOptions>(options =>
			{
				options.SuppressModelStateInvalidFilter = true;
			});

			// Add services
			builder.Services.AddLogging(options =>
			{
				options.ClearProviders();
				options.AddSerilog();
			});
			builder.Services.AddSingleton(appSettings);
			builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
			builder.Services.AddMemoryCache();
			builder.Services.AddEndpointsApiExplorer();

			// Custom extensions
			builder.Services.ConfigureRateLimiting();
			builder.Services.ConfigureCorsPolicies();
			builder.Services.ConfigureSwagger();
			builder.Services.ConfigureJWTAuthenticate(configuration);


			var app = builder.Build();

			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger(c => c.OpenApiVersion = Microsoft.OpenApi.OpenApiSpecVersion.OpenApi2_0);
				app.UseSwaggerUI();
			}
			app.UseMiddleware<IPRateLimitingMiddleware>(); // Custom middleware for handling IP rate limiting for specific endpoints
			app.UseIpRateLimiting();
			app.UseHttpsRedirection();
			app.UseCors("AllowOrigins");
			app.UseAuthentication();
			app.UseAuthorization();
			app.MapControllers();
			app.Run();
		}
	}
}
