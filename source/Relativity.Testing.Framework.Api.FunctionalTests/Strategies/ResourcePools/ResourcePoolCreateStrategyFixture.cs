using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateStrategy<ResourcePool>))]
	[Ignore("It looks like these tests are causing other tests to have trouble getting the correct resource pool, needs to be investigated. RTF-753")]
	internal class ResourcePoolCreateStrategyFixture : ApiServiceTestFixture<ICreateStrategy<ResourcePool>>
	{
		[Test]
		public void Create_WithFilledEntity()
		{
			if (Facade.Resolve<IVersionRangeMatchService>().IsVersionInRange(Facade.RelativityInstanceVersion, "<= 11.2"))
			{
				Assert.Inconclusive();
			}

			Client client = null;

			Arrange(x => x.Create(out client));

			var entity = new ResourcePool
			{
				Name = Randomizer.GetString("AT_"),
				Client = client
			};

			var result = Sut.Create(entity);

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.
				Excluding(x => x.ArtifactID).
				Excluding(x => x.Client));
		}
	}
}
