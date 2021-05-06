using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Configuration;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MotdHasDismissedStrategy : IMotdHasDismissedStrategy
	{
		private readonly IConfigurationService _configurationService;
		private readonly IRestService _restService;
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;

		public MotdHasDismissedStrategy(
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

			var dto = new
			{
				userId
			};

			return _restService.Post<bool>("Relativity.Services.Notifications.INotificationsModule/Notifications/HasDismissedMOTDAsync", dto);
		}
	}
}
