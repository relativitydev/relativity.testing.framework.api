using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<Layout>))]
	[VersionRange(">=12.0")]
	[Ignore("https://github.com/relativitydev/relativity.testing.framework.api/issues/13")]
	internal class LayoutGetEligibleOwnersStrategyFixture : ApiServiceTestFixture<ILayoutGetEligibleOwnersStrategy>
	{
		[Test]
		public void GetEligibleOwners()
		{
			var result = Sut.GetEligibleOwners(DefaultWorkspace.ArtifactID);

			result.Should().NotBeNullOrEmpty();
		}
	}
}
