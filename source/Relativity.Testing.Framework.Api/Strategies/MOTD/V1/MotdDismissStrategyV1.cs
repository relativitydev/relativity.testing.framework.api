﻿using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Configuration;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class MotdDismissStrategyV1 : IMotdDismissStrategy
	{
		private readonly IConfigurationService _configurationService;
		private readonly IRestService _restService;
		private readonly IUserGetByEmailStrategy _userGetByEmailStrategy;

		public MotdDismissStrategyV1(
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

			var url = BuildUrl(userId.Value);

			_restService.Post(url);
		}

		public void Dismiss(string emailAddress)
		{
			int userId = _userGetByEmailStrategy.Get(emailAddress).ArtifactID;

			Dismiss(userId);
		}

		private string BuildUrl(int userId)
		{
			return $"relativity-infrastructure/v1/workspaces/-1/notifications/dismiss/{userId}";
		}
	}
}
