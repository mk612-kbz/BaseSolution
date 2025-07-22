using CommonApi.application.Interfaces.Services;
using CommonApi.application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace CommonApi.application
{
	public static class DependencyInjection
	{
		public static void AddApplication(this IServiceCollection services)
		{
			services.AddScoped<IAuditLogService, AuditLogService>();

			services.AddAutoMapper(cfg => cfg.AddMaps(AppDomain.CurrentDomain.GetAssemblies()));

		}
	}
}
