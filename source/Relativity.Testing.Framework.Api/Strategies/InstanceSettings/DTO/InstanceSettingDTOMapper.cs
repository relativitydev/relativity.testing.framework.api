using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.InstanceSettings
{
	internal static class InstanceSettingDTOMapper
	{
		internal static InstanceSetting DoMappingFromDTO(this InstanceSettingDTO value)
		{
			var mapped = new InstanceSetting
			{
				Name = value.Name,
				ArtifactID = value.ArtifactID,
				Machine = value.Machine,
				Description = value.Description,
				InitialValue = value.InitialValue,
				Section = value.Section,
				Value = value.Value
			};

			if (value.ValueType != null)
			{
				mapped.ValueType = (InstanceSettingValueType)value.ValueType.Value;
			}

			return mapped;
		}

		internal static InstanceSettingDTO DoMappingToDTO(this InstanceSetting value)
		{
			var mapped = new InstanceSettingDTO
			{
				Name = value.Name,
				ArtifactID = value.ArtifactID,
				Machine = value.Machine,
				Description = value.Description,
				InitialValue = value.InitialValue,
				Section = value.Section,
				Value = value.Value,
			};

			if (value.ValueType != null)
			{
				mapped.ValueType = (InstanceSettingValueTypeDTOV1)value.ValueType.Value;
			}

			return mapped;
		}
	}
}
