using kCura.Relativity.DataReaderClient;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the REST HTTP service that provides NOTHING.
	/// </summary>
	public interface IImportApiService
	{
		ImageImportBulkArtifactJob NewImageImportJob();

		ImportBulkArtifactJob NewNativeDocumentImportJob();

		ImageImportBulkArtifactJob NewProductionImportJob(int productionId);
	}
}
