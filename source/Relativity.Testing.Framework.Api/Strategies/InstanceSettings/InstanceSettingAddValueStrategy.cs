using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class InstanceSettingAddValueStrategy : IInstanceSettingAddValueStrategy
	{
		private readonly IUpdateStrategy<InstanceSetting> _updateStrategy;
		private readonly IInstanceSettingGetByNameAndSectionStrategy _instanceSettingGetByNameAndSectionStrategy;

		public InstanceSettingAddValueStrategy(
			IUpdateStrategy<InstanceSetting> updateStrategy,
			IInstanceSettingGetByNameAndSectionStrategy instanceSettingGetByNameAndSectionStrategy)
		{
			_updateStrategy = updateStrategy;
			_instanceSettingGetByNameAndSectionStrategy = instanceSettingGetByNameAndSectionStrategy;
		}

		public void AddValue(string name, string section, string value, string delimiter)
		{
			var entity = _instanceSettingGetByNameAndSectionStrategy.Get(name, section);

			entity.Value += string.IsNullOrEmpty(entity.Value) ? value : delimiter + value;

			_updateStrategy.Update(entity);
		}
	}
}
