using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class InstanceSettingsService : IInstanceSettingsService
	{
		private readonly ICreateStrategy<InstanceSetting> _createStrategy;
		private readonly IRequireStrategy<InstanceSetting> _requireStrategy;
		private readonly IInstanceSettingRequireByDefaultArgumentsStrategy _instanceSettingRequireByDefaultArgumentsStrategy;
		private readonly IDeleteByIdStrategy<InstanceSetting> _deleteByIdStrategy;
		private readonly IGetByIdStrategy<InstanceSetting> _getByIdStrategy;
		private readonly IInstanceSettingGetByNameAndSectionStrategy _instanceSettingGetByNameAndSectionStrategy;
		private readonly IUpdateStrategy<InstanceSetting> _updateStrategy;
		private readonly IInstanceSettingUpdateValueStrategy _instanceSettingUpdateValueStrategy;
		private readonly IInstanceSettingAddValueStrategy _instanceSettingAddValueStrategy;
		private readonly IInstanceSettingRequireContainsValueStrategy _instanceRequireContainsStrategy;
		private readonly IInstanceSettingRequireNotContainsValueStrategy _instanceRequireNotContainsStrategy;

		public InstanceSettingsService(
			ICreateStrategy<InstanceSetting> createStrategy,
			IRequireStrategy<InstanceSetting> requireStrategy,
			IInstanceSettingRequireByDefaultArgumentsStrategy instanceSettingRequireByDefaultArgumentsStrategy,
			IDeleteByIdStrategy<InstanceSetting> deleteByIdStrategy,
			IGetByIdStrategy<InstanceSetting> getByIdStrategy,
			IInstanceSettingGetByNameAndSectionStrategy instanceSettingGetByNameAndSectionStrategy,
			IUpdateStrategy<InstanceSetting> updateStrategy,
			IInstanceSettingUpdateValueStrategy instanceSettingUpdateValueStrategy,
			IInstanceSettingAddValueStrategy instanceSettingAddValueStrategy,
			IInstanceSettingRequireContainsValueStrategy instanceRequireContainsStrategy,
			IInstanceSettingRequireNotContainsValueStrategy instanceRequireNotContainsStrategy)
		{
			_createStrategy = createStrategy;
			_requireStrategy = requireStrategy;
			_instanceSettingRequireByDefaultArgumentsStrategy = instanceSettingRequireByDefaultArgumentsStrategy;
			_deleteByIdStrategy = deleteByIdStrategy;
			_getByIdStrategy = getByIdStrategy;
			_instanceSettingGetByNameAndSectionStrategy = instanceSettingGetByNameAndSectionStrategy;
			_updateStrategy = updateStrategy;
			_instanceSettingUpdateValueStrategy = instanceSettingUpdateValueStrategy;
			_instanceSettingAddValueStrategy = instanceSettingAddValueStrategy;
			_instanceRequireContainsStrategy = instanceRequireContainsStrategy;
			_instanceRequireNotContainsStrategy = instanceRequireNotContainsStrategy;
		}

		public InstanceSetting Create(InstanceSetting entity)
			=> _createStrategy.Create(entity);

		public InstanceSetting Require(InstanceSetting entity)
			=> _requireStrategy.Require(entity);

		public InstanceSetting Require(string name, string section, string value)
			=> _instanceSettingRequireByDefaultArgumentsStrategy.Require(name, section, value);

		public void Delete(int id)
			=> _deleteByIdStrategy.Delete(id);

		public InstanceSetting Get(int id)
			=> _getByIdStrategy.Get(id);

		public InstanceSetting Get(string name, string section)
			=> _instanceSettingGetByNameAndSectionStrategy.Get(name, section);

		public void Update(InstanceSetting entity)
			=> _updateStrategy.Update(entity);

		public void UpdateValue(string name, string section, string value)
			=> _instanceSettingUpdateValueStrategy.UpdateValue(name, section, value);

		public void AddValue(string name, string section, string value, string delimiter)
			=> _instanceSettingAddValueStrategy.AddValue(name, section, value, delimiter);

		public void RequireContainsValue(string name, string section, string value, string delimiter)
			=> _instanceRequireContainsStrategy.RequireContainsValue(name, section, value, delimiter);

		public void RequireNotContainsValue(string name, string section, string value, string delimiter)
			=> _instanceRequireNotContainsStrategy.RequireNotContainsValue(name, section, value, delimiter);
	}
}
