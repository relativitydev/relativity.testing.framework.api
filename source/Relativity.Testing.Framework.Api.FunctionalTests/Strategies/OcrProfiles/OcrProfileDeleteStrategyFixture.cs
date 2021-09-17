using System.Net.Http;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteWorkspaceEntityByIdStrategy<OcrProfile>))]
	internal class OcrProfileDeleteStrategyFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<OcrProfile>>
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
			OcrProfile toDelete = null;

			ArrangeWorkingWorkspace(x => x.Create(new OcrProfile()).Pick(out toDelete));

			Sut.Delete(DefaultWorkspace.ArtifactID, toDelete.ArtifactID);

			var exception = Assert.Throws<HttpRequestException>(() => Facade.Resolve<IGetWorkspaceEntityByIdStrategy<ObjectType>>()
																			.Get(DefaultWorkspace.ArtifactID, toDelete.ArtifactID));

			Assert.IsTrue(exception.Message.StartsWith("StatusCode: 404, ReasonPhrase: 'Not Found'"));
		}
	}
}
