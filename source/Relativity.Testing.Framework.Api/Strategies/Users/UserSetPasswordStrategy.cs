using System.Net.Http;
using Relativity.Testing.Framework.Api.Attributes;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Logging;

namespace Relativity.Testing.Framework.Api.Strategies.Users
{
	[DoNotLogging]
	internal class UserSetPasswordStrategy : IUserSetPasswordStrategy
	{
		private readonly IRestService _restService;
		private readonly ILogService _logService;

		public UserSetPasswordStrategy(IRestService restService, ILogService logService)
		{
			_restService = restService;
			_logService = logService;
		}

		public void SetPassword(int userArtifactID, string password)
		{
			AddPasswordProvider(userArtifactID);
			DoSetPassword(userArtifactID, password);
		}

		private void AddPasswordProvider(int userArtifactId)
		{
			var passwordProviderProfile = new
			{
				Profile = new
				{
					userId = userArtifactId,
					Password = new
					{
						IsEnabled = true,
						MustResetPasswordOnNextLogin = false
					}
				}
			};

			_restService.Post("Relativity.Services.Security.ISecurityModule/Login Profile Manager/SaveLoginProfileAsync", passwordProviderProfile);
		}

		private void DoSetPassword(int userArtifactId, string passwordToSet)
		{
			var password = new
			{
				UserId = userArtifactId,
				Password = passwordToSet
			};

			try
			{
				_restService.Post("Relativity.Services.Security.ISecurityModule/Login Profile Manager/SetPasswordAsync", password);
			}
			catch (HttpRequestException ex) when (ex.Message.Contains("Cannot reuse a previous password"))
			{
				_logService.Info($"Password for user {userArtifactId} could not be updated because it matched a previous password");
			}
		}
	}
}
