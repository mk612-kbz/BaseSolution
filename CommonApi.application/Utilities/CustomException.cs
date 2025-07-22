using CommonApi.Application.Common;

namespace CommonApi.application.Utilities
{
	public class CustomException : Exception
	{
		public ErrorInfo ErrorInfo { get; }
		public int StatusCode { get; }

		public CustomException(ErrorInfo errorCode, string message, int statusCode) : base(message)
		{
			ErrorInfo = GenerateMessage(errorCode, message, null, null);
			StatusCode = statusCode;
		}

		public CustomException(ErrorInfo errorCode, string message, int statusCode, List<ErrorDetails> detailList) : base(message)
		{
			ErrorInfo = GenerateMessage(errorCode, message, detailList, null);
			StatusCode = statusCode;
		}

		public CustomException(ErrorInfo errorCode, string message, int statusCode, Exception innerException) : base(message, innerException)
		{
			ErrorInfo = GenerateMessage(errorCode, message, null, innerException);
			StatusCode = statusCode;
		}

		private static ErrorInfo GenerateMessage(ErrorInfo errorInfo, string message, List<ErrorDetails>? errorList, Exception? ex)
		{
			ErrorInfo error = new ErrorInfo();
			try
			{
				error = errorInfo;

				if (error.Details == null)
				{
					if (errorList != null && errorList.Count > 0)
					{
						error.Details = errorList;
					}
					else
					{
						error.Details = new List<ErrorDetails>();

						ErrorDetails detail = new ErrorDetails();
						detail.ErrorCode = "";

						if (ex != null)
							detail.ErrorDescription = ex.Message;

						else
							detail.ErrorDescription = message;

						error.Details.Add(detail);
					}
				}
			}
			catch (Exception)
			{
				errorInfo = ErrorCode.UnknownException;
			}

			return errorInfo;
		}
	}
}
