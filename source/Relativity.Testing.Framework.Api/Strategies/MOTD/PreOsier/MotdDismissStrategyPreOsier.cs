using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Configuration;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class MotdDismissStrategyPreOsier : IMotdDismissStrategy
	{
		private const string _POST_URL = "Relativity.Services.Notifications.INotificationsModule/Notifications/DismissMOTDAsync";
		private readonly IConfigurationService _configurationService;
		private readonly IRestService _restService;
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;

		public MotdDismissStrategyPreOsier(
			IConfigurationService configurationService,
			IRestService restService,
			IUserGetByEmailStrategy userGetByEmailStrategy)
		{
			_configurationService = configurationService;
			_restService = restService;
			_userGetByEmailStrategy = userGetByEmailStrategy;
		}

		public void Dismiss(int? userId = null)
		{
			var dto = BuildDto(userId);

			_restService.Post(_POST_URL, dto);
		}

		public void Dismiss(string emailAddress)
		{
			int userId = _userGetByEmailStrategy.Get(emailAddress).ArtifactID;

			Dismiss(userId);
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
