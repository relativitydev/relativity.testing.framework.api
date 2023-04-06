using System.Linq;
using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteWorkspaceEntityByIdStrategy<Document>))]
	internal class DocumentDeleteByIdStrategyFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<Document>>
	{
		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(DefaultWorkspace.ArtifactID, int.MaxValue));
		}

		[Test]
		public void Delete_Existing()
		{
			Workspace workspace = null;
			IDocumentService documentService = Facade.Resolve<IDocumentService>();

			Arrange(domain =>
			{
				domain.Create(out workspace);
				documentService.ImportGeneratedDocuments(workspace.ArtifactID, 2);
			});

			Document documentToDelete = documentService.GetAll(workspace.ArtifactID).FirstOrDefault();

			Sut.Delete(workspace.ArtifactID, documentToDelete.ArtifactID);

			Document[] result = documentService.GetAll(workspace.ArtifactID);

			result.Should().NotContain(documentToDelete);
		}
	}
}
