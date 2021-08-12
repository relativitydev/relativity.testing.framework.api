using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies.Tabs;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ITabGetEligibleParentsStrategy))]
	[VersionRange(">=12.1")]
	internal class TabGetEligibleParentsStrategyV1Fixture : ApiServiceTestFixture<ITabGetEligibleParentsStrategy>
	{
		[Test]
		public void Get_AllForDefault()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID);

			result.Should().NotBeEmpty();
		}
	}
}
