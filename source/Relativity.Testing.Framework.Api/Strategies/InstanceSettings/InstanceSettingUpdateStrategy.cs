using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class InstanceSettingUpdateStrategy : IUpdateStrategy<InstanceSetting>
	{
		private readonly IRestService _restService;
		private readonly IInstanceSettingGetByNameAndSectionStrategy _instanceSettingGetByNameAndSectionStrategy;

		public InstanceSettingUpdateStrategy(IRestService restService, IInstanceSettingGetByNameAndSectionStrategy instanceSettingGetByNameAndSectionStrategy)
		{
			_restService = restService;
			_instanceSettingGetByNameAndSectionStrategy = instanceSettingGetByNameAndSectionStrategy;
		}

		public void Update(InstanceSetting entity)
		{
			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID == 0)
			{
				if (entity.Name != null && entity.Section != null)
				{
					entity.ArtifactID = _instanceSettingGetByNameAndSectionStrategy.Get(entity.Name, entity.Section).ArtifactID;
				}
				else
				{
					throw new ArgumentException("This entity should have an artifact ID or name and section as an identifier.", nameof(entity));
				}
			}

			var dto = new
			{
				instanceSetting = entity
			};

			_restService.Put(
				"Relativity.InstanceSettings/workspace/-1/instancesettings",
				dto);
		}
	}
}
