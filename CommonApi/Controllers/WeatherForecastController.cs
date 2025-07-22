using CommonApi.application.Common;
using CommonApi.application.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace CommonApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class WeatherForecastController : BaseController
	{
		private static readonly string[] Summaries = new[]
		{
			"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
		};

		private readonly ILogger<WeatherForecastController> _logger;

		public WeatherForecastController(ILogger<WeatherForecastController> logger, AppSettings appSettings, IAuditLogService auditLogService) : base(appSettings, auditLogService)
		{
			_logger = logger;
		}

		[HttpGet("GetWeatherForecast")]
		public IEnumerable<WeatherForecast> Get()
		{
			return Enumerable.Range(1, 5).Select(index => new WeatherForecast
			{
				Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
				TemperatureC = Random.Shared.Next(-20, 55),
				Summary = Summaries[Random.Shared.Next(Summaries.Length)]
			})
			.ToArray();
		}
	}
}
