using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Configuration;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MotdDismissStrategy : IMotdDismissStrategy
	{
		private readonly IConfigurationService _configurationService;
		private readonly IRestService _restService;
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;

		public MotdDismissStrategy(
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
			if (userId == null)
			{
				userId = _userGetByEmailStrategy.Get(_configurationService.RelativityInstance.AdminUsername).ArtifactID;
			}

			var dto = new
			{
				userId
			};

			_restService.Post("Relativity.Services.Notifications.INotificationsModule/Notifications/DismissMOTDAsync", dto);
		}
	}
}
