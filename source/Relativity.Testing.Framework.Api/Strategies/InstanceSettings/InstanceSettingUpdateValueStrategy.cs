using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class InstanceSettingUpdateValueStrategy : IInstanceSettingUpdateValueStrategy
	{
		private readonly IUpdateStrategy<InstanceSetting> _updateStrategy;

		public InstanceSettingUpdateValueStrategy(IUpdateStrategy<InstanceSetting> updateStrategy)
		{
			_updateStrategy = updateStrategy;
		}

		public void UpdateValue(string name, string section, string value)
		{
			_updateStrategy.Update(new InstanceSetting { Name = name, Section = section, Value = value });
		}
	}
}
