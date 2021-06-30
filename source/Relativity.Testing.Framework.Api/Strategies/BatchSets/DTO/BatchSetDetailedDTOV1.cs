using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetDetailedDTOV1 : IBatchSetDTOV1
	{
		public BatchSetDetailedDTOV1()
		{
		}

		public BatchSetDetailedDTOV1(BatchSet batchSet, BatchSetDetailedDTOV1 batchSetDto)
		{
			ValidateBatchSet(batchSet);

			Name = batchSet.Name;
			BatchPrefix = batchSet.BatchPrefix;
			BatchSize = batchSet.BatchSize;
			DataSource = new DataSourceDTOV1
			{
				Value = batchSet.DataSource
			};
			BatchSetID = batchSet.ArtifactID;
			Keywords = batchSet.Keywords;
			Notes = batchSet.Notes;
			LastModifiedOn = batchSetDto.LastModifiedOn;
			LastModifiedBy = batchSetDto.LastModifiedBy;
			CreatedBy = batchSetDto.CreatedBy;
			CreatedOn = batchSetDto.CreatedOn;
			AutoBatchProgress = batchSetDto.AutoBatchProgress;
		}

		public int BatchSetID { get; set; }

		public string Name { get; set; }

		public string BatchPrefix { get; set; }

		public int BatchSize { get; set; }

		public DataSourceDTOV1 DataSource { get; set; }

		public string Keywords { get; set; }

		public string Notes { get; set; }

		public object AutoBatchProgress { get; set; }

		public DateTime CreatedOn { get; set; }

		public DateTime LastModifiedOn { get; set; }

		public NamedArtifact CreatedBy { get; set; }

		public NamedArtifact LastModifiedBy { get; set; }

		public BatchSet Map()
		{
			var mapped = new BatchSet
			{
				Name = Name,
				BatchPrefix = BatchPrefix,
				BatchSize = BatchSize,
				DataSource = DataSource.Value,
				ArtifactID = BatchSetID,
				Notes = Notes,
				Keywords = Keywords
			};
			return mapped;
		}

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
