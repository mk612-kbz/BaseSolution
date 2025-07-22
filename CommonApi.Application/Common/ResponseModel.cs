namespace CommonApi.application.Common
{
	public class ResponseModel
	{
		public string? KBZRefNo { get; set; }
		[Newtonsoft.Json.JsonProperty(NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public dynamic? Data { get; set; }

		[Newtonsoft.Json.JsonProperty(NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public ErrorInfo? Error { get; set; }
	}

	public class ResponseMessage
	{
		public string? Id { get; set; }
		public string? Message { get; set; }
	}

	public class DocumentResponseMessage
	{
		public string? DocumentNo { get; set; }
	}
}
