using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IQueryResourcesStrategy))]
	internal class ResourcePoolQueryResourcesStrategyFixture : ApiServiceTestFixture<IQueryResourcesStrategy>
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

			var agent = resourcePool.AgentAndWorkerServers.First();

			var result = Sut.Query(resourcePool.ArtifactID)
				.Where(x => x.ArtifactID, agent.ArtifactID)
				.FirstOrDefault();

			result.Status.Should().NotBeNull();
			result.Type.Should().BeEquivalentTo(ResourceServerType.Agent);
			result.Should().BeEquivalentTo(agent);
		}
	}
}
