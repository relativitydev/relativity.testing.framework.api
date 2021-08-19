using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ITabGetAdminLevelMetadataStrategy))]
	[VersionRange(">=12.1")]
	internal class TabGetAdminLevelMetadataStrategyV1Fixture : ApiServiceTestFixture<ITabGetAdminLevelMetadataStrategy>
	{
		[Test]
		public void Get_NotThrowException()
		{
			Assert.DoesNotThrow(() => Sut.Get());
		}
	}
}
