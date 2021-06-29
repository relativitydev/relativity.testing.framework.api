using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetDTOMapStrategyV1 : IBatchSetDTOMapStrategyV1
	{
		public BatchSet Map(BatchSetDetailedDTOV1 batchSetDto)
		{
			var mapped = new BatchSet
			{
				Name = batchSetDto.Name,
				BatchPrefix = batchSetDto.BatchPrefix,
				BatchSize = batchSetDto.BatchSize,
				DataSource = batchSetDto.DataSource.Value,
				ArtifactID = batchSetDto.BatchSetID,
				Notes = batchSetDto.Notes,
				Keywords = batchSetDto.Keywords
			};
			return mapped;
		}
	}
}
