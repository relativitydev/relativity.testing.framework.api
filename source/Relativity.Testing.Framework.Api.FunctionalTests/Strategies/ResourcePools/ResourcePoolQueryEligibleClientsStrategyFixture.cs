using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IQueryEligibleToAddClientsStrategy))]
	internal class ResourcePoolQueryEligibleClientsStrategyFixture : ApiServiceTestFixture<IQueryEligibleToAddClientsStrategy>
	{
		[Test]
		public void Query()
		{
			var result = Sut.Query().SetLength(5)
				.ToArray();

			result.Should().NotBeNullOrEmpty();

			foreach (var client in result)
			{
				client.ArtifactID.Should().BeGreaterThan(0);
				client.Name.Should().NotBeNullOrEmpty();
			}
		}
	}
}
