using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<Layout>))]
	[VersionRange(">=12.0")]
	internal class LayoutGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<Layout>>
	{
		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Layout existingTab = null;

			ArrangeWorkingWorkspace(x => x.Create(new Layout()).Pick(out existingTab));

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingTab.ArtifactID);

			result.Should().BeEquivalentTo(existingTab);
		}
	}
}
