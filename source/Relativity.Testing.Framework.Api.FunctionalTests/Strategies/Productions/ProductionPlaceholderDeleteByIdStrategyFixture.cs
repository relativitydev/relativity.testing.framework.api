using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteWorkspaceEntityByIdStrategy<ProductionPlaceholder>))]
	internal class ProductionPlaceholderDeleteByIdStrategyFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<ProductionPlaceholder>>
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
			ProductionPlaceholder toDelete = null;

			Arrange(() =>
			{
				toDelete = Facade.Resolve<ICreateWorkspaceEntityStrategy<ProductionPlaceholder>>()
					.Create(DefaultWorkspace.ArtifactID, new ProductionPlaceholder());
			});

			Sut.Delete(DefaultWorkspace.ArtifactID, toDelete.ArtifactID);

			Facade.Resolve<IGetWorkspaceEntityByIdStrategy<ProductionPlaceholder>>()
				.Get(DefaultWorkspace.ArtifactID, toDelete.ArtifactID).Should().BeNull();
		}
	}
}
