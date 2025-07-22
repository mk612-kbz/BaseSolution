namespace CommonApi.application.Common
{
	public class ErrorInfo
	{
		public string? Code { get; set; }
		public string? Message { get; set; }
		[Newtonsoft.Json.JsonProperty(NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore)]
		public List<ErrorDetails>? Details { get; set; }

	}

	public class ErrorDetails
	{
		public string? ErrorCode { get; set; }
		public string? ErrorDescription { get; set; }
	}

	public class ErrorCode
	{
		public static ErrorInfo Unauthorized { get { return new ErrorInfo { Code = "1000", Message = "Unauthorized." }; } }
		public static ErrorInfo ValidationError { get { return new ErrorInfo { Code = "1001", Message = "Validation error" }; } }
		public static ErrorInfo DatabaseError { get { return new ErrorInfo { Code = "1002", Message = "Database Error." }; } }
		public static ErrorInfo NoRowsAffected { get { return new ErrorInfo { Code = "1003", Message = "No Rows Affected." }; } }
		public static ErrorInfo NoRecordFound { get { return new ErrorInfo { Code = "1004", Message = "No Record Found!" }; } }
		public static ErrorInfo DuplicateRecord { get { return new ErrorInfo { Code = "1005", Message = "Duplicate Record!" }; } }
		public static ErrorInfo UnknownException { get { return new ErrorInfo { Code = "1006", Message = "Indicate unknown exception." }; } }
		public static ErrorInfo ThirdParty_Error { get { return new ErrorInfo { Code = "1007", Message = "Can't connect AD Server." }; } }
		public static ErrorInfo RequestTimeOut { get { return new ErrorInfo { Code = "1008", Message = "Request timeout error." }; } }
		public static ErrorInfo GatewayTimeout { get { return new ErrorInfo { Code = "1008", Message = "Gateway timeout error." }; } }
		public static ErrorInfo StatusIsNotUpdate { get { return new ErrorInfo { Code = "1009", Message = "This action is not allow." }; } }
		public static ErrorInfo StatusAlreadyUpdated { get { return new ErrorInfo { Code = "1010", Message = "This document's action already updated." }; } }
		public static ErrorInfo ExcelGenerateError { get { return new ErrorInfo { Code = "1011", Message = "Excel file generate fail." }; } }
		public static ErrorInfo ChangeRequestFormAlreadyOpen { get { return new ErrorInfo { Code = "1011", Message = "This client is already opened Changed Request Document" }; } }
		public static ErrorInfo GroupMappingAlreadyOpen { get { return new ErrorInfo { Code = "1011", Message = "This group is already opened Client Mapping Document" }; } }
		public static ErrorInfo AuditLogError { get { return new ErrorInfo { Code = "1012", Message = "Audit Log Error." }; } }
		public static ErrorInfo TargetSystemError { get { return new ErrorInfo { Code = "1016", Message = "Target system response error." }; } }
		public static ErrorInfo InvalidToken { get { return new ErrorInfo { Code = "1000", Message = "Unauthorized.", Details = new List<ErrorDetails> { ErrorCodeDetail.InvalidToken } }; } }
	}

	public class ErrorCodeDetail
	{
		public static ErrorDetails Invalid_LoginID { get { return new ErrorDetails { ErrorCode = "103000001", ErrorDescription = "Invalid Staff ID." }; } }
		public static ErrorDetails OperationError { get { return new ErrorDetails { ErrorCode = "103000002", ErrorDescription = "Indicate unknown exception in System Layer" }; } }
		public static ErrorDetails OperationErrorBiz { get { return new ErrorDetails { ErrorCode = "103000002", ErrorDescription = "Indicate unknown exception in Business Layer" }; } }
		public static ErrorDetails Invalid_UserName_Pwd { get { return new ErrorDetails { ErrorCode = "103000003", ErrorDescription = "Invalid username or password" }; } }
		public static ErrorDetails Validate_ProductType { get { return new ErrorDetails { ErrorCode = "103000004", ErrorDescription = "Product Type should not be blank." }; } }
		public static ErrorDetails Validate_LoginID { get { return new ErrorDetails { ErrorCode = "103000004", ErrorDescription = "LoginID should not be blank." }; } }
		public static ErrorDetails Denied { get { return new ErrorDetails { ErrorCode = "103000005", ErrorDescription = "User can't access this system." }; } }
		public static ErrorDetails Expired { get { return new ErrorDetails { ErrorCode = "103000006", ErrorDescription = "Your password has expired." }; } }
		public static ErrorDetails TokenExpired { get { return new ErrorDetails { ErrorCode = "103000007", ErrorDescription = "Your token has expired." }; } }
		public static ErrorDetails InvalidToken { get { return new ErrorDetails { ErrorCode = "103000008", ErrorDescription = "Invalid Token" }; } }
		public static ErrorDetails Duplicate_NRC { get { return new ErrorDetails { ErrorCode = "SYS003", ErrorDescription = "{0}" }; } }
		public static ErrorDetails Duplicate_Phone { get { return new ErrorDetails { ErrorCode = "SYS011", ErrorDescription = "{0}" }; } }
		public static ErrorDetails UserNoExist { get { return new ErrorDetails { ErrorCode = "SYS007", ErrorDescription = "{0}" }; } }
		public static ErrorDetails Duplicate_Email { get { return new ErrorDetails { ErrorCode = "SYS008", ErrorDescription = "{0}" }; } }
		public static ErrorDetails VerifyEmail { get { return new ErrorDetails { ErrorCode = "SYS009", ErrorDescription = "{0}" }; } }
		public static ErrorDetails IsKYCUpdate { get { return new ErrorDetails { ErrorCode = "SYS004", ErrorDescription = "{0}" }; } }
		public static ErrorDetails IsRegisterRequested { get { return new ErrorDetails { ErrorCode = "SYS005", ErrorDescription = "{0}" }; } }
		public static ErrorDetails NotMatch_Email { get { return new ErrorDetails { ErrorCode = "SYS001", ErrorDescription = "{0}" }; } }
		public static ErrorDetails RequiredPwd { get { return new ErrorDetails { ErrorCode = "SYS006", ErrorDescription = "{0}" }; } }
		public static ErrorDetails NTBUser { get { return new ErrorDetails { ErrorCode = "SYS010", ErrorDescription = "{0}" }; } }
		public static ErrorDetails FrozenUser { get { return new ErrorDetails { ErrorCode = "SYS012", ErrorDescription = "{0}" }; } }
		public static ErrorDetails Duplicate_Data { get { return new ErrorDetails { ErrorCode = "SYS013", ErrorDescription = "{0}" }; } }
		public static ErrorDetails RequiredBioData { get { return new ErrorDetails { ErrorCode = "SYS014", ErrorDescription = "{0}" }; } }
		public static ErrorDetails InvalidUserPwd { get { return new ErrorDetails { ErrorCode = "SYS015", ErrorDescription = "{0}" }; } }
		public static ErrorDetails Invalid_emailToken { get { return new ErrorDetails { ErrorCode = "SYS016", ErrorDescription = "{0}" }; } }
		public static ErrorDetails Ald_ActivatedLogin { get { return new ErrorDetails { ErrorCode = "SYS017", ErrorDescription = "{0}" }; } }
		public static ErrorDetails Duplicate_Application { get { return new ErrorDetails { ErrorCode = "SYS018", ErrorDescription = "{0}" }; } }
		public static ErrorDetails Duplicate_Id { get { return new ErrorDetails { ErrorCode = "SYS019", ErrorDescription = "{0}" }; } }
		public static ErrorDetails ExceedApplication { get { return new ErrorDetails { ErrorCode = "SYS020", ErrorDescription = "{0}" }; } }
		public static ErrorDetails InvalidRequest { get { return new ErrorDetails { ErrorCode = "SYS021", ErrorDescription = "{0}" }; } }
		public static ErrorDetails PasswordDecryptFailed { get { return new ErrorDetails { ErrorCode = "103000046", ErrorDescription = "Authentication Request not valid." }; } }
		public static ErrorDetails InvalidRequestParameter { get { return new ErrorDetails { ErrorCode = "103000020", ErrorDescription = "Mandatory field not found." }; } }
		public static ErrorDetails OperationErrorDetail { get { return new ErrorDetails { ErrorCode = "103000044", ErrorDescription = "{0}" }; } }
		public static ErrorDetails LDAPException { get { return new ErrorDetails { ErrorCode = "LDAP001", ErrorDescription = "{0}" }; } }
		public static ErrorDetails AD_Timeout { get { return new ErrorDetails { ErrorCode = "103000043", ErrorDescription = "Request to AD Server was timeout." }; } }
		public static ErrorDetails ThirdParty_ErrorDetail { get { return new ErrorDetails { ErrorCode = "103000021", ErrorDescription = "{0}" }; } }
		public static ErrorDetails Data_Not_Found { get { return new ErrorDetails { ErrorCode = "103000009", ErrorDescription = "Data Not found." }; } }
		public static ErrorDetails Some_Client_InActive { get { return new ErrorDetails { ErrorCode = "103000010", ErrorDescription = "Some client is InActive." }; } }
		public static ErrorDetails GroupAlreadyOpened { get { return new ErrorDetails { ErrorCode = "103000011", ErrorDescription = "This group already open document." }; } }
	}
}
