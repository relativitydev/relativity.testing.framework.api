using System;
using kCura.Relativity.DataReaderClient;

namespace Relativity.Testing.Framework.Api.Services
{
    internal class ImportApiService : IImportApiService
    {
        public ImageImportBulkArtifactJob NewImageImportJob()
        {
            throw new NotImplementedException();
        }

        public ImportBulkArtifactJob NewNativeDocumentImportJob()
        {
            throw new NotImplementedException();
        }

        public ImageImportBulkArtifactJob NewProductionImportJob(int productionId)
        {
            throw new NotImplementedException();
        }
    }
}
