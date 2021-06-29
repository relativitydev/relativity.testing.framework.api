using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class BatchSetDetailedDTOV1 : IBatchSetDTOV1
	{
		public BatchSetDetailedDTOV1()
		{
		}

		public BatchSetDetailedDTOV1(BatchSet batchSet)
		{
			Name = batchSet.Name;
			BatchPrefix = batchSet.BatchPrefix;
			BatchSize = batchSet.BatchSize;
			DataSource = new DataSourceDTOV1
			{
				Value = batchSet.DataSource
			};
			BatchSetID = batchSet.ArtifactID;
			Keywords = batchSet.Keywords;
			Notes = batchSet.Keywords;
			LastModifiedOn = DateTime.UtcNow;
		}

		public int BatchSetID { get; set; }

		public string Name { get; set; }

		public string BatchPrefix { get; set; }

		public int BatchSize { get; set; }

		public DataSourceDTOV1 DataSource { get; set; }

		public string Keywords { get; set; }

		public string Notes { get; set; }

		public DateTime LastModifiedOn { get; set; }
	}
}
