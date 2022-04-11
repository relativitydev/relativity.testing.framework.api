using Relativity.Testing.Framework.Api.Services;

namespace Relativity.Testing.Framework.Api.Strategies.Users
{
	internal class UserSetPasswordStrategy : IUserSetPasswordStrategy
	{
		private readonly IRestService _restService;

		public UserSetPasswordStrategy(IRestService restService)
		{
			_restService = restService;
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

			_restService.Post("Relativity.Services.Security.ISecurityModule/Login Profile Manager/SetPasswordAsync", password);
		}
	}
}
