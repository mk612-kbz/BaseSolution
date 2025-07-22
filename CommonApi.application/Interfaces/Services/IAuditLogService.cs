using CommonApi.application.Common;

namespace CommonApi.application.Interfaces.Services
{
	public interface IAuditLogService
	{
		void AuditThread(AuditLogModel logModel);
	}
}
