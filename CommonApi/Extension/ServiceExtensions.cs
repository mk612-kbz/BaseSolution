using AspNetCoreRateLimit;
using CommonApi.application.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Security.Cryptography.X509Certificates;

namespace CommonApi.Extension
{
	public static class ServiceExtensions
	{
		public static void ConfigureRateLimiting(this IServiceCollection services)
		{
			services.Configure<IpRateLimitOptions>(options =>
			{
				options.EnableEndpointRateLimiting = true;
				options.StackBlockedRequests = false;
				options.HttpStatusCode = 429;
				options.RealIpHeader = "X-Forwarded-For";
				options.ClientIdHeader = "X-ClientId";
				options.DisableRateLimitHeaders = true;
				options.GeneralRules = new List<RateLimitRule>
			{
				new RateLimitRule
				{
					Endpoint = "*",
					Period = "1m",
					Limit = 10
				}
			};
			});

			services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
			services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
			services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
			services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
			services.AddInMemoryRateLimiting();
		}
		public static void ConfigureCorsPolicies(this IServiceCollection services)
		{
			services.AddCors(options =>
			{
				options.AddPolicy("AllowOrigins",
					builder => builder.AllowAnyOrigin()
									  .WithMethods("OPTIONS", "GET", "POST", "PUT")
									  .AllowAnyHeader());
			});
		}
		public static void ConfigureSwagger(this IServiceCollection services)
		{
			services.AddSwaggerGen(opt =>
			{
				opt.SwaggerDoc("v1", new OpenApiInfo
				{

					Title = "Common API",
					Version = "1.0.0",
					Description = "API documentation for Common API",
					Contact = new OpenApiContact
					{
						Name = "John Doe",
						Email = "john.doe@kbzbank.com"
					}
				});

				opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter 'Bearer' [space] and then your valid token in the text input below."
				});

				opt.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						new OpenApiSecurityScheme
						{
							Reference = new OpenApiReference
							{
								Type = ReferenceType.SecurityScheme,
								Id = "Bearer"
							}
						},
						Array.Empty<string>()
					}
				});
			});

			services.AddSwaggerGenNewtonsoftSupport();
		}
		public static void ConfigureJWTAuthenticate(this IServiceCollection services, IConfiguration configuration)
		{
			var thumbPrint = configuration.GetSection("AppSettings").GetSection("JwtConfig").GetSection("ThumbPrint").Value;
			if (!string.IsNullOrEmpty(thumbPrint))
			{
				SecurityKey key = new X509SecurityKey(GetCertificateFromStore(thumbPrint));

				services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
				.AddJwtBearer(option =>
				{
					option.TokenValidationParameters = new TokenValidationParameters
					{
						IssuerSigningKey = key,
						ValidateIssuer = true,
						ValidateAudience = true,
						ValidateIssuerSigningKey = true,
						ValidIssuer = configuration.GetSection("AppSettings").GetSection("JwtConfig").GetSection("Issuer").Value,
						ValidAudience = configuration.GetSection("AppSettings").GetSection("JwtConfig").GetSection("AudienceId").Value,
						ValidateLifetime = true,
						ClockSkew = TimeSpan.Zero
					};
					option.Events = new JwtBearerEvents()
					{
						OnChallenge = async context =>
						{
							context.HandleResponse();
							context.Response.StatusCode = 401;
							context.Response.ContentType = "application/json";
							context.HttpContext.Request.Headers.TryGetValue("KBZ_REF_NO", out var LOGID);
							await Task.FromResult(context.Response.WriteAsJsonAsync<ResponseModel>(new ResponseModel { KBZRefNo = LOGID, Error = ErrorCode.InvalidToken }));
						}
					};
				});
			}
		}
		private static X509Certificate2? GetCertificateFromStore(string thumbprint)
		{
			X509Store store = new X509Store(StoreLocation.LocalMachine);
			try
			{
				store.Open(OpenFlags.ReadOnly);
				X509Certificate2Collection certCollection = store.Certificates;
				X509Certificate2Collection currentCerts = certCollection.Find(X509FindType.FindByTimeValid, DateTime.Now, false);
				X509Certificate2Collection signingCert = currentCerts.Find(X509FindType.FindByThumbprint, thumbprint, false);
				if (signingCert.Count == 0)
				{
					return null;
				}
				else
				{
					return signingCert[0];
				}
			}
			finally
			{
				store.Close();
			}
		}
	}
}
