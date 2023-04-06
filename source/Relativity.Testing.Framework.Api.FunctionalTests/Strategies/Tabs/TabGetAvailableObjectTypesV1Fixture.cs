using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ITabGetAvailableObjectTypesByWorkspaceIDStrategy))]
	[VersionRange(">=12.1")]
	internal class TabGetAvailableObjectTypesV1Fixture : ApiServiceTestFixture<ITabGetAvailableObjectTypesByWorkspaceIDStrategy>
	{
		[Test]
		public void Get_AllForDefault()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID);

			result.Should().NotBeEmpty();
		}
	}
}
