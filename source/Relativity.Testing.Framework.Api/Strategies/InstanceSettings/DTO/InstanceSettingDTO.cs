namespace Relativity.Testing.Framework.Api.Strategies.InstanceSettings
{
	internal class InstanceSettingDTO
	{
		public int ArtifactID { get; set; }

		public string Name { get; set; }

		public string Machine { get; set; }

		public string Description { get; set; }

		public string InitialValue { get; set; }

		public string Section { get; set; }

		public string Value { get; set; }

		public InstanceSettingValueTypeDTOV1? ValueType { get; set; }

		public string Keywords { get; set; }

		public bool Encrypted { get; set; }
	}
}
