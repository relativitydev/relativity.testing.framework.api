namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetRequestV1
	{
		public BatchSetRequestV1(IBatchSetDTOV1 batchSetDto)
		{
			BatchSet = batchSetDto;
		}

		public IBatchSetDTOV1 BatchSet { get; set; }
	}
}
