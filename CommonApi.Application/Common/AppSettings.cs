namespace CommonApi.application.Common
{
	public class AppSettings
	{
		public JwtConfig? JwtConfig { get; set; }
		public bool APILog { get; set; }
	}

	public class JwtConfig
	{
		public required string ThumbPrint { get; set; }
		public required string Issuer { get; set; }
		public required string AudienceId { get; set; }
		public required string ClaimUserNameKey { get; set; }
	}
}
