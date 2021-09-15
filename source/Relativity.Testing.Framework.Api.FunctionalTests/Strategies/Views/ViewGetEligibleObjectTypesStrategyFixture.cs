using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IViewGetEligibleObjectTypesStrategy))]
	internal class ViewGetEligibleObjectTypesStrategyFixture : ApiServiceTestFixture<IViewGetEligibleObjectTypesStrategy>
	{
		[Test]
		public void GetAll()
		{
			var result = Sut.GetEligibleObjectTypes(DefaultWorkspace.ArtifactID);

			result.Should().NotBeNullOrEmpty();
		}
	}
}
