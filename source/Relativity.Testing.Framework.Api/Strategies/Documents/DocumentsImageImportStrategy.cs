using System.Data;
using kCura.Relativity.DataReaderClient;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentsImageImportStrategy : DocumentImportHelper, IDocumentsImageImportStrategy
	{
		public DocumentsImageImportStrategy(IImportApiService importApiService)
			: base(importApiService)
		{
		}

		public void Import(int workspaceId, DataTable dataTable, ImageImportOptions options = null)
		{
			ImageImportBulkArtifactJob importBulkArtifactJob = ImportApiService.NewImageImportJob();
			ImageImportConfigurations.ConfigureImportJobSettingsForImageImport(importBulkArtifactJob, workspaceId, options);

			importBulkArtifactJob.SourceData.SourceData = dataTable;

			SubscribeToImportJob(importBulkArtifactJob);
			importBulkArtifactJob.Execute();
			UnsubscribeFromImportJob(importBulkArtifactJob);
		}
	}
}
