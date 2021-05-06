using System;
using System.Linq;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class InstanceSettingRequireNotContainsValueStrategy : IInstanceSettingRequireNotContainsValueStrategy
	{
		private readonly IDeleteByIdStrategy<InstanceSetting> _deleteStrategy;
		private readonly IInstanceSettingUpdateValueStrategy _updateStrategy;
		private readonly IInstanceSettingGetByNameAndSectionStrategy _instanceSettingGetByNameAndSectionStrategy;

		public InstanceSettingRequireNotContainsValueStrategy(
			IDeleteByIdStrategy<InstanceSetting> deleteStrategy,
			IInstanceSettingUpdateValueStrategy updateStrategy,
			IInstanceSettingGetByNameAndSectionStrategy instanceSettingGetByNameAndSectionStrategy)
		{
			_deleteStrategy = deleteStrategy;
			_updateStrategy = updateStrategy;
			_instanceSettingGetByNameAndSectionStrategy = instanceSettingGetByNameAndSectionStrategy;
		}

		public void RequireNotContainsValue(string name, string section, string value, string delimiter)
		{
			InstanceSetting instanceSetting = _instanceSettingGetByNameAndSectionStrategy.Get(name, section);
			if (instanceSetting != null)
			{
				if (instanceSetting.Value.Equals(value))
				{
					_deleteStrategy.Delete(instanceSetting.ArtifactID);
				}
				else
				{
					string newValue = string.Join(delimiter, instanceSetting.Value.Split(new[] { delimiter }, StringSplitOptions.RemoveEmptyEntries).Where(x => x != value));
					_updateStrategy.UpdateValue(name, section, newValue);
				}
			}
		}
	}
}
