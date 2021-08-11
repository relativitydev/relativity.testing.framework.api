using System.Data;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Exceptions;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies.Documents
{
	[TestOf(typeof(DocumentImportHelper))]
	internal class DocumentImportHelperFixture : ApiServiceTestFixture<IDocumentSingleNativeImportStrategy>
	{
		[Test]
		public void DocumentImportHelper_WhenInvalidDataTable_ThrowsJobReportException()
		{
			var documentService = Facade.Resolve<IDocumentService>();
			DataTable dataTable = new DataTable();
			Assert.Throws<JobReportException>(() => documentService.ImportNatives(DefaultWorkspace.ArtifactID, dataTable));
			dataTable.Dispose();
		}
	}
}
