using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Configuration;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class MotdHasDismissedStrategyV1 : IMotdHasDismissedStrategy
	{
		private readonly IConfigurationService _configurationService;
		private readonly IRestService _restService;
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;

		public MotdHasDismissedStrategyV1(
			IConfigurationService configurationService,
			IRestService restService,
			IUserGetByEmailStrategy userGetByEmailStrategy)
		{
			_configurationService = configurationService;
			_restService = restService;
			_userGetByEmailStrategy = userGetByEmailStrategy;
		}

		public bool HasDismissed(int? userId = null)
		{
			if (userId == null)
			{
				userId = _userGetByEmailStrategy.Get(_configurationService.RelativityInstance.AdminUsername).ArtifactID;
			}

			var url = BuildUrl(userId.Value);

			return _restService.Get<bool>(url);
		}

		public async Task<bool> HasDismissedAsync(int? userId = null)
		{
			if (userId == null)
			{
				userId = _userGetByEmailStrategy.Get(_configurationService.RelativityInstance.AdminUsername).ArtifactID;
			}

			var url = BuildUrl(userId.Value);

			return await _restService.GetAsync<bool>(url).ConfigureAwait(false);
		}

		private string BuildUrl(int userId)
		{
			return $"relativity-infrastructure/v1/workspaces/-1/notifications/has-dismissed/{userId}";
		}
	}
}
