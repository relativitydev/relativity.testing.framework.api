using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Configuration;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class MotdHasDismissedStrategyPreOsier : IMotdHasDismissedStrategy
	{
		private const string _POST_URL = "Relativity.Services.Notifications.INotificationsModule/Notifications/HasDismissedMOTDAsync";
		private readonly IConfigurationService _configurationService;
		private readonly IRestService _restService;
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;

		public MotdHasDismissedStrategyPreOsier(
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
			var dto = BuildDto(userId);

			return _restService.Post<bool>(_POST_URL, dto);
		}

		private object BuildDto(int? userId = null)
		{
			if (userId == null)
			{
				userId = _userGetByEmailStrategy.Get(_configurationService.RelativityInstance.AdminUsername).ArtifactID;
			}

			return new
			{
				userId
			};
		}
	}
}
