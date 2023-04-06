using System;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.InstanceSettings
{
	internal abstract class InstanceSettingUpdateAbstractStrategy : IUpdateStrategy<InstanceSetting>
	{
		private readonly IInstanceSettingGetByNameAndSectionStrategy _instanceSettingGetByNameAndSectionStrategy;

		protected InstanceSettingUpdateAbstractStrategy(IInstanceSettingGetByNameAndSectionStrategy instanceSettingGetByNameAndSectionStrategy)
		{
			_instanceSettingGetByNameAndSectionStrategy = instanceSettingGetByNameAndSectionStrategy;
		}

		public virtual void Update(InstanceSetting entity)
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

			DoUpdate(entity);
		}

		protected abstract void DoUpdate(InstanceSetting entity);
	}
}
