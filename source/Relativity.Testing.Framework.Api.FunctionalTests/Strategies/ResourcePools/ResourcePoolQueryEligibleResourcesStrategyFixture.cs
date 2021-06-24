using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IQueryEligibleToAddResourcesStrategy))]
	internal class ResourcePoolQueryEligibleResourcesStrategyFixture : ApiServiceTestFixture<IQueryEligibleToAddResourcesStrategy>
	{
		private IGetAllStrategy<ResourcePool> _getAllStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			if (Facade.Resolve<IVersionRangeMatchService>().IsVersionInRange(Facade.RelativityInstanceVersion, "<= 11.2"))
			{
				Assert.Inconclusive();
			}

			_getAllStrategy = Facade.Resolve<IGetAllStrategy<ResourcePool>>();
		}

		[Test]
		public void Query()
		{
			var resourcePool = _getAllStrategy.GetAll().First(x => x.AgentAndWorkerServers != null);

			var result = Sut.Query(resourcePool.ArtifactID)
				.ToArray();

			result.Should().NotBeNullOrEmpty();

			foreach (var server in result)
			{
				server.ArtifactID.Should().BeGreaterThan(0);
				server.Name.Should().NotBeNullOrEmpty();
				server.Status.Should().NotBeNull();
			}
		}
	}
}
