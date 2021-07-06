using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.InstanceSettings
{
	internal static class InstanceSettingDTOMapper
	{
		internal static InstanceSetting DoMapping(this InstanceSettingDTO value)
		{
			var mapped = new InstanceSetting
			{
				Name = value.Name,
				ArtifactID = value.ArtifactID,
				Machine = value.Machine,
				Description = value.Description,
				InitialValue = value.InitialValue,
				Section = value.Section,
				Value = value.Value,
				ValueType = (InstanceSettingValueType)value.ValueType.Value,
			};
			return mapped;
		}
	}
}
