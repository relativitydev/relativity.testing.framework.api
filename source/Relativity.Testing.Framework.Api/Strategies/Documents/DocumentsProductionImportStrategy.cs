using System.Data;
using kCura.Relativity.DataReaderClient;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentsProductionImportStrategy : DocumentImportHelper, IDocumentsProductionImportStrategy
	{
		public DocumentsProductionImportStrategy(IImportApiService importApiService)
			: base(importApiService)
		{
		}

		public void Import(int workspaceId, int productionId, DataTable dataTable, ImageImportOptions options = null)
		{
			ImageImportBulkArtifactJob importBulkArtifactJob = ImportApiService.NewProductionImportJob(productionId);
			ImageImportConfigurations.ConfigureImportJobSettingsForImageImport(importBulkArtifactJob, workspaceId, options);

			importBulkArtifactJob.SourceData.SourceData = dataTable;

			SubscribeToImportJob(importBulkArtifactJob);
			importBulkArtifactJob.Execute();
			UnsubscribeFromImportJob(importBulkArtifactJob);
		}
	}
}
