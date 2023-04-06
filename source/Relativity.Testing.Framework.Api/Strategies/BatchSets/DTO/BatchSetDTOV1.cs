using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetDTOV1 : IBatchSetDTOV1
	{
		public BatchSetDTOV1(BatchSet batchSet)
		{
			ValidateBatchSet(batchSet);

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

		private static void ValidateBatchSet(BatchSet batchSet)
		{
			if (batchSet == null)
			{
				throw new ArgumentNullException(nameof(batchSet));
			}

			if (batchSet.DataSource == null || batchSet.DataSource.ArtifactID < 1)
			{
				throw new ArgumentException("Data Source must not be null and have Artifact ID greater than zero");
			}

			if (string.IsNullOrWhiteSpace(batchSet.Name))
			{
				throw new ArgumentException("Batch Set Name cannot be null, empty nor whitespace.");
			}
		}
	}
}
