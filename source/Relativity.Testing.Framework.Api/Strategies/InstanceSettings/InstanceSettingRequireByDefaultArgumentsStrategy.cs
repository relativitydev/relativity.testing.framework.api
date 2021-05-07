using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class InstanceSettingRequireByDefaultArgumentsStrategy : IInstanceSettingRequireByDefaultArgumentsStrategy
	{
		private readonly IRequireStrategy<InstanceSetting> _requireStrategy;

		public InstanceSettingRequireByDefaultArgumentsStrategy(
			IRequireStrategy<InstanceSetting> requireStrategy)
		{
			_requireStrategy = requireStrategy;
		}

		public InstanceSetting Require(string name, string section, string value)
		{
			return _requireStrategy.Require(new InstanceSetting { Name = name, Section = section, Value = value });
		}
	}
}
