using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IResourcePoolAddResourceStrategy))]
	[Ignore("It looks like these tests are causing other tests to have trouble getting the correct resource pool, needs to be investigated. RTF-753")]
	internal class ResourcePoolAddRemoveResourceStrategyFixture : ApiServiceTestFixture<IResourcePoolAddResourceStrategy>
	{
		private ResourcePool _resourcePool;
		private IGetByIdStrategy<ResourcePool> _getByIdStrategy;
		private IGetAllStrategy<ResourceServer> _getAllStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			if (Facade.Resolve<IVersionRangeMatchService>().IsVersionInRange(Facade.RelativityInstanceVersion, "<= 11.2"))
			{
				Assert.Inconclusive();
			}

			_getByIdStrategy = Facade.Resolve<IGetByIdStrategy<ResourcePool>>();
			_getAllStrategy = Facade.Resolve<IGetAllStrategy<ResourceServer>>();
			Client client = null;

			Arrange(x => x.Create(out client));

			var entity = new ResourcePool
			{
				Name = Randomizer.GetString("AT_"),
				Client = client
			};

			Arrange(x => x.Create(entity).Pick(out _resourcePool));
		}

		[Test]
		[Order(1)]
		public void Add()
		{
			var servers = _getAllStrategy.GetAll().Where(x => x.Type == ResourceServerType.Agent).Cast<Artifact>().ToList();
			Sut.Add(_resourcePool.ArtifactID, ResourceType.AgentWorkerServers, servers);

			var result = _getByIdStrategy.Get(_resourcePool.ArtifactID);

			result.AgentAndWorkerServers.Should().BeEquivalentTo(servers);
		}

		[Test]
		[Order(2)]
		public void Remove()
		{
			var servers = _getAllStrategy.GetAll().Where(x => x.Type == ResourceServerType.Agent).Cast<Artifact>().ToList();

			Facade.Resolve<IResourcePoolRemoveResourceStrategy>()
				.Remove(_resourcePool.ArtifactID, ResourceType.AgentWorkerServers, servers);

			var result = _getByIdStrategy.Get(_resourcePool.ArtifactID);

			result.AgentAndWorkerServers.Should().BeNull();
		}
	}
}
