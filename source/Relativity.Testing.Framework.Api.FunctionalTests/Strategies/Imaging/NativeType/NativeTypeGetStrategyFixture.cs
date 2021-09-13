using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(INativeTypeGetStrategy))]
	internal class NativeTypeGetStrategyFixture : ApiServiceTestFixture<INativeTypeGetStrategy>
	{
		private const int ValidNativeTypeId = 1035846;

		[Test]
		public void Get_WithValidNativeTypeId_ShouldBeSuccessful()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, ValidNativeTypeId);

			result.Should().NotBeNull();
			result.ArtifactID.Should().BePositive();
		}
	}
}
