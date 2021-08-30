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
		public void Get_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(DefaultWorkspace.ArtifactID, int.MaxValue));
		}

		[Test]
		public void Get_Existing()
		{
			var documentService = Facade.Resolve<IDocumentService>();
			var documentToDelete = documentService.GetAll(DefaultWorkspace.ArtifactID).First();

			Sut.Delete(DefaultWorkspace.ArtifactID, documentToDelete.ArtifactID);

			var result = documentService.GetAll(DefaultWorkspace.ArtifactID);

			result.Should().NotContain(documentToDelete);
		}
	}
}
