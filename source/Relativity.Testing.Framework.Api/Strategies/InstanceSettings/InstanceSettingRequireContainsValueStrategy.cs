using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class InstanceSettingRequireContainsValueStrategy : IInstanceSettingRequireContainsValueStrategy
	{
		private readonly ICreateStrategy<InstanceSetting> _createStrategy;
		private readonly IInstanceSettingAddValueStrategy _addValueStrategy;
		private readonly IInstanceSettingGetByNameAndSectionStrategy _instanceSettingGetByNameAndSectionStrategy;

		public InstanceSettingRequireContainsValueStrategy(
			ICreateStrategy<InstanceSetting> createStrategy,
			IInstanceSettingAddValueStrategy addValueStrategy,
			IInstanceSettingGetByNameAndSectionStrategy instanceSettingGetByNameAndSectionStrategy)
		{
			_createStrategy = createStrategy;
			_addValueStrategy = addValueStrategy;
			_instanceSettingGetByNameAndSectionStrategy = instanceSettingGetByNameAndSectionStrategy;
		}

		public void RequireContainsValue(string name, string section, string value, string delimiter)
		{
			InstanceSetting instanceSetting = _instanceSettingGetByNameAndSectionStrategy.Get(name, section);
			if (instanceSetting == null)
			{
				_createStrategy.Create(new InstanceSetting
				{
					Name = name,
					Section = section,
					Value = value
				});
			}
			else
			{
				if (!instanceSetting.Value.Contains(value))
				{
					_addValueStrategy.AddValue(name, section, value, delimiter);
				}
			}
		}
	}
}
