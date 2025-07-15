using Microsoft.Extensions.Caching.Memory;

namespace CommonApi.Middleware
{
	public class IPRateLimitingMiddleware
	{
		private readonly RequestDelegate _next;
		private readonly IMemoryCache _cache;
		private const string RateLimitKey = "LongTermRateLimit_";

		public IPRateLimitingMiddleware(RequestDelegate next, IMemoryCache cache)
		{
			_next = next;
			_cache = cache;
		}

		public async Task InvokeAsync(HttpContext context)
		{
			if (context.Request.Path.Value != null && context.Request.Path.Value.Equals("/api/WeatherForecast/GetWeatherForecast", StringComparison.OrdinalIgnoreCase)
				&& context.Request.Method.Equals("GET", StringComparison.OrdinalIgnoreCase)) // To configure custom limit for specific endpoint
			{
				string? clientIp = context.Connection.RemoteIpAddress?.ToString();
				string cacheKey = RateLimitKey + clientIp;

				int requestCount = _cache.GetOrCreate(cacheKey, entry =>
				{
					entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(1)); // 1-minute window
					return 0;
				});

				requestCount++;

				if (requestCount > 20)
				{
					context.Response.StatusCode = 503;
					context.Response.ContentType = "text/plain; charset=utf-8";
					await context.Response.WriteAsync("Services Unavailable.");
					return;
				}

				_cache.Set(cacheKey, requestCount, TimeSpan.FromMinutes(1));
			}

			await _next(context);
		}
	}
}
