using Microsoft.Extensions.DependencyInjection;

namespace CommonApi.infrastructure
{
	public static class DependencyInjection
	{
		public static void AddInfrastructure(this IServiceCollection services)
		{
			// Register your infrastructure services here
			// For example, if you have a DbContext or other services, register them here
			// Example:
			// services.AddDbContext<MyDbContext>(options =>
			//     options.UseSqlServer("YourConnectionString"));
			// You can also register repositories, external API clients, etc.
			// services.AddScoped<IMyRepository, MyRepository>();
		}
	}
}
