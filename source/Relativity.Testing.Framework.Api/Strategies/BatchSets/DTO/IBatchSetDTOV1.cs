namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IBatchSetDTOV1
	{
		public string Name { get; set; }

		public string BatchPrefix { get; set; }

		public int BatchSize { get; set; }
	}
}
