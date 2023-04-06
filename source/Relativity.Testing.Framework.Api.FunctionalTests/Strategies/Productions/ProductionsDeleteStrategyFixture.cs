using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteWorkspaceEntityByIdStrategy<Production>))]
	internal class ProductionsDeleteStrategyFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<Production>>
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
			Production toDelete = null;

			Arrange(() =>
			{
				toDelete = Facade.Resolve<ICreateWorkspaceEntityStrategy<Production>>()
					.Create(DefaultWorkspace.ArtifactID, new Production());
			});

			Sut.Delete(DefaultWorkspace.ArtifactID, toDelete.ArtifactID);

			Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Production>>().Get(DefaultWorkspace.ArtifactID, toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
