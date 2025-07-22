namespace CommonApi.application.Common
{
	public class CommonEnums
	{
		public enum AuditLogLevel
		{
			DEBUG,
			INFO,
			WARN,
			ERROR,
			FATAL
		}
		public enum HttpVerb
		{
			POST,
			GET,
			PUT,
			PATCH,
			DELETE
		}
		public enum PayLoadType
		{
			REQUEST,
			RESPONSE
		}
	}
}
