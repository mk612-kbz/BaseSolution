using CommonApi.application.Common;
using CommonApi.application.Interfaces.Services;
using CommonApi.application.Utilities;
using CommonApi.application.Common;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;

namespace CommonApi.Controllers
{
	public class BaseController(AppSettings appSettings, IAuditLogService auditLogService) : ControllerBase
	{
		protected string kbzRefNo = string.Empty;
		protected string? claimUser = string.Empty;

		// To extract the KBZ_REF_NO from the request headers if it exists
		[ApiExplorerSettings(IgnoreApi = true)]
		public void AssignLogID()
		{
			if (string.IsNullOrEmpty(kbzRefNo))
			{
				Request.Headers.TryGetValue("KBZ_REF_NO", out var LOGID);

				if (Request.Headers.ContainsKey("KBZ_REF_NO"))
				{
					kbzRefNo = ((IList<String>)LOGID)[0].ToString();
				}
				else
				{
					kbzRefNo = Guid.NewGuid().ToString();
				}
			}
		}

		// To extract the username from the JWT token in the request headers
		[ApiExplorerSettings(IgnoreApi = true)]
		public void GetClaimUsername()
		{
			try
			{
				string? token = GetTokenFromHeader();
				if (string.IsNullOrEmpty(token))
				{
					claimUser = null;
					return;
				}

				claimUser = ExtractClaimFromToken(token, "user_name");
			}
			catch
			{
				claimUser = null;
			}
		}

		// To get user information from the request headers. This method isn't mandatory for all projects, but it is used in H2H project.
		[ApiExplorerSettings(IgnoreApi = true)]
		public UserInfo GetUserInfo()
		{
			try
			{
				var userInfo = new UserInfo
				{
					LoginStaffId = Request.Headers["LoginStaffID"].ToString(),
					StageMasterId = Request.Headers["StageMasterID"].ToString(),
					ProductType = Request.Headers["ProductType"].ToString()
				};

				if (string.IsNullOrEmpty(userInfo.LoginStaffId) || string.IsNullOrEmpty(userInfo.StageMasterId) || string.IsNullOrEmpty(userInfo.ProductType))
				{
					throw new CustomException(ErrorCode.Unauthorized, "", (int)HttpStatusCode.Unauthorized);
				}
				else
				{
					return userInfo;
				}
			}
			catch
			{
				throw new CustomException(ErrorCode.Unauthorized, "", (int)HttpStatusCode.Unauthorized, new List<ErrorDetails> { ErrorCodeDetail.Validate_LoginID });
			}
		}

		// To log audit information. This method is used to log API calls and other significant events.
		[ApiExplorerSettings(IgnoreApi = true)]
		public void LogAudit(AuditLogModel logModel)
		{
			try
			{
				if (appSettings.APILog)
				{
					logModel.AuditLogID = Guid.NewGuid().ToString();
					logModel.LoggedBy = claimUser ?? "";
					logModel.SourceUrl = new Uri($"{Request.Scheme}://{Request.Host}{Request.Path}{Request.QueryString}").AbsoluteUri;
					logModel.CurrentUrl = HttpContext.Request.GetEncodedUrl();
					logModel.KBZMessageID = string.IsNullOrEmpty(logModel.KBZMessageID) ? Guid.NewGuid().ToString() : logModel.KBZMessageID;
					logModel.LoggedDate = DateTime.Now;
					auditLogService.AuditThread(logModel);
				}

			}
			catch
			{
				throw new CustomException(ErrorCode.AuditLogError, "", (int)HttpStatusCode.BadRequest);
			}
		}

		#region Private methods
		private string? GetTokenFromHeader()
		{
			if (!Request.Headers.TryGetValue("Authorization", out var authorizationHeaderValue))
			{
				return null;
			}

			var authorizationHeader = AuthenticationHeaderValue.Parse(authorizationHeaderValue);
			return authorizationHeader.Parameter;
		}

		private string? ExtractClaimFromToken(string token, string claimType)
		{
			var handler = new JwtSecurityTokenHandler();
			var jsonToken = handler.ReadToken(token) as JwtSecurityToken;

			return jsonToken?.Claims.FirstOrDefault(claim => claim.Type == claimType)?.Value;
		}

		#endregion
	}
}
