using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetAllWorkspaceViewOwnersStrategy<NamedArtifact>))]
	internal class ViewGetViewOwnersStrategyFixture : ApiServiceTestFixture<IGetAllWorkspaceViewOwnersStrategy<NamedArtifact>>
	{
		[Test]
		public void GetAll()
		{
			var result = Sut.GetViewOwners(DefaultWorkspace.ArtifactID);

			result.Should().NotBeNullOrEmpty();
		}
	}
}
