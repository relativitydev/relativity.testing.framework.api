using System.Data;
using kCura.Relativity.DataReaderClient;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DocumentsNativeImportStrategy : DocumentImportHelper, IDocumentsNativeImportStrategy
	{
		public DocumentsNativeImportStrategy(IImportApiService importApiService)
			: base(importApiService)
		{
		}

		public void Import(int workspaceId, DataTable dataTable, NativeImportOptions options = null)
		{
			ImportBulkArtifactJob importBulkArtifactJob = ImportApiService.NewNativeDocumentImportJob();
			NativeImportConfigurations.ConfigureImportJobSettingsForNativeImport(importBulkArtifactJob, workspaceId, options);

			importBulkArtifactJob.SourceData.SourceData = dataTable.CreateDataReader();

			SubscribeToImportJob(importBulkArtifactJob);
			importBulkArtifactJob.Execute();
			UnsubscribeFromImportJob(importBulkArtifactJob);
		}
	}
}
