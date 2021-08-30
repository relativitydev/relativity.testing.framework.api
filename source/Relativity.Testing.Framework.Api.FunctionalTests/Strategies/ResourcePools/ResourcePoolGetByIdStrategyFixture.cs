using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ResourcePoolGetByIdStrategy))]
	[Parallelizable(ParallelScope.None)]
	internal class ResourcePoolGetByIdStrategyFixture : ApiServiceTestFixture<IGetByIdStrategy<ResourcePool>>
	{
		private IGetAllStrategy<ResourcePool> _getAllStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			_getAllStrategy = Facade.Resolve<IGetAllStrategy<ResourcePool>>();
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			var getAllResourcePools = _getAllStrategy.GetAll().First();

			var result = Sut.Get(getAllResourcePools.ArtifactID);

			result.Should().Should().NotBeNull();
			result.ArtifactID.Should().BePositive();
		}
	}
}
