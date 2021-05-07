using System;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class InstanceSettingRequireStrategy : IRequireStrategy<InstanceSetting>
	{
		private readonly ICreateStrategy<InstanceSetting> _createStrategy;
		private readonly IUpdateStrategy<InstanceSetting> _updateStrategy;
		private readonly IInstanceSettingGetByNameAndSectionStrategy _instanceSettingGetByNameAndSectionStrategy;
		private readonly IGetByIdStrategy<InstanceSetting> _getByIdStrategy;

		public InstanceSettingRequireStrategy(
			ICreateStrategy<InstanceSetting> createStrategy,
			IUpdateStrategy<InstanceSetting> updateStrategy,
			IInstanceSettingGetByNameAndSectionStrategy instanceSettingGetByNameAndSectionStrategy,
			IGetByIdStrategy<InstanceSetting> getByIdStrategy)
		{
			_createStrategy = createStrategy;
			_updateStrategy = updateStrategy;
			_instanceSettingGetByNameAndSectionStrategy = instanceSettingGetByNameAndSectionStrategy;
			_getByIdStrategy = getByIdStrategy;
		}

		public InstanceSetting Require(InstanceSetting entity)
		{
			InstanceSetting instanceSetting = null;

			if (entity is null)
			{
				throw new ArgumentNullException(nameof(entity));
			}

			if (entity.ArtifactID == 0)
			{
				if (entity.Name != null && entity.Section != null)
				{
					instanceSetting = _instanceSettingGetByNameAndSectionStrategy.Get(entity.Name, entity.Section);
				}
				else
				{
					throw new ArgumentException("This entity should have an artifact ID or name and section as an identifier.", nameof(entity));
				}
			}
			else
			{
				instanceSetting = entity;
			}

			if (instanceSetting == null)
			{
				instanceSetting = _createStrategy.Create(entity);
			}
			else
			{
				entity.ArtifactID = instanceSetting.ArtifactID;
				_updateStrategy.Update(entity);
				instanceSetting = _getByIdStrategy.Get(entity.ArtifactID);
			}

			return instanceSetting;
		}
	}
}
