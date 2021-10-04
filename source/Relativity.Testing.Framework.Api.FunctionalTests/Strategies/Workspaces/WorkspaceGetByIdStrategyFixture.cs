using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetByIdStrategy<Workspace>))]
	internal class WorkspaceGetByIdStrategyFixture : ApiServiceTestFixture<IGetByIdStrategy<Workspace>>
	{
		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		[VersionRange("<12.1")]
		public void Get_Existing_PreOsier()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID);

			result.Should().BeEquivalentTo(DefaultWorkspace);
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Get_Existing_V1()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID);

			result.Should().BeEquivalentTo(DefaultWorkspace, o => o
			   .Excluding(x => x.Client.ArtifactID)
			   .Excluding(x => x.Matter.ArtifactID));
		}
	}
}
