using System.Net;
using static CommonApi.application.Common.CommonEnums;

namespace CommonApi.application.Common
{
	public class AuditLogModel
	{
		public string AuditLogID { get; set; }

		public DateTime LoggedDate { get; set; }

		public object PayLoad { get; set; }

		public string? LoggedBy { get; set; }

		public PayLoadType PayLoadType { get; set; }

		public string TransactionRefNo { get; set; }

		public string KBZMessageID { get; set; }

		public string SourceUrl { get; set; }

		public string CurrentUrl { get; set; }

		public HttpVerb HttpVerb { get; set; }

		public HttpStatusCode HttpCode { get; set; }

		public string? Message { get; set; }

		public AuditLogLevel LogLevel { get; set; }

		public string Exception { get; set; }

		public string ServiceCategory { get; set; }

		public string ServiceName { get; set; }

		public string ServiceStatus { get; set; }

		public string SupportKey { get; set; }

		public string ResponseCode { get; set; }

		public AuditLogModel(object payLoad, string? loggedBy, PayLoadType payLoadType, string kBZMessageID,
			HttpVerb httpVerb, HttpStatusCode httpCode, string? message, AuditLogLevel logLevel, string exception,
			string serviceCategory)
		{
			PayLoad = payLoad;
			LoggedBy = loggedBy;
			PayLoadType = payLoadType;
			KBZMessageID = kBZMessageID;
			HttpVerb = httpVerb;
			HttpCode = httpCode;
			Message = message;
			LogLevel = logLevel;
			Exception = exception;
			ServiceName = "H2H";
			ServiceCategory = serviceCategory;
		}
	}
}
