using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(KeywordSearchDeleteByIdStrategy))]
	internal class KeywordSearchDeleteStrategyFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<KeywordSearch>>
	{
		public KeywordSearchDeleteStrategyFixture()
		{
		}

		public KeywordSearchDeleteStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(DefaultWorkspace.ArtifactID, int.MaxValue));
		}

		[Test]
		public void Delete_Existing()
		{
			KeywordSearch toDelete = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out toDelete));

			Sut.Delete(DefaultWorkspace.ArtifactID, toDelete.ArtifactID);

			Facade.Resolve<IGetWorkspaceEntityByIdStrategy<KeywordSearch>>().Get(DefaultWorkspace.ArtifactID, toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
