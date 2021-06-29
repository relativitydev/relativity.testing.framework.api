using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetDTOV1 : IBatchSetDTOV1
	{
		public BatchSetDTOV1(BatchSet batchSet)
		{
			Name = batchSet.Name;
			BatchPrefix = batchSet.BatchPrefix;
			BatchSize = batchSet.BatchSize;
			DataSource = new DataSourceDTOV1
			{
				Value = new NamedArtifact
				{
					ArtifactID = batchSet.DataSource.ArtifactID
				}
			};
		}

		public string Name { get; set; }

		public string BatchPrefix { get; set; }

		public int BatchSize { get; set; }

		public DataSourceDTOV1 DataSource { get; set; }
	}
}
