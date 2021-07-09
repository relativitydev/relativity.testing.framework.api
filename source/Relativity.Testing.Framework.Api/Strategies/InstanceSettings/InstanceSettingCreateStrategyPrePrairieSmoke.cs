﻿using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class InstanceSettingCreateStrategyPrePrairieSmoke : CreateStrategy<InstanceSetting>
	{
		private readonly IRestService _restService;
		private readonly IGetByIdStrategy<InstanceSetting> _getByIdStrategy;

		public InstanceSettingCreateStrategyPrePrairieSmoke(
			IRestService restService,
			IGetByIdStrategy<InstanceSetting> getByIdStrategy)
		{
			_restService = restService;
			_getByIdStrategy = getByIdStrategy;
		}

		protected override InstanceSetting DoCreate(InstanceSetting entity)
		{
			var dto = new
			{
				instanceSetting = entity
			};

			var artifactId = _restService.Post<int>(
				"Relativity.InstanceSettings/workspace/-1/instancesettings",
				dto);

			return _getByIdStrategy.Get(artifactId);
		}
	}
}
