using System;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateStrategy<ResourcePool>))]
	internal class ResourcePoolUpdateStrategyFixture : ApiServiceTestFixture<IUpdateStrategy<ResourcePool>>
	{
		private IGetByIdStrategy<ResourcePool> _getByIdStrategy;
		private IGetAllStrategy<ResourcePool> _getAllStrategy;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			if (Facade.Resolve<IVersionRangeMatchService>().IsVersionInRange(Facade.RelativityInstanceVersion, "<= 11.2"))
			{
				Assert.Inconclusive();
			}

			_getByIdStrategy = Facade.Resolve<IGetByIdStrategy<ResourcePool>>();
			_getAllStrategy = Facade.Resolve<IGetAllStrategy<ResourcePool>>();
		}

		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(null));
		}

		[Test]
		public void Update()
		{
			var getallResourcePool = _getAllStrategy.GetAll().FirstOrDefault();

			var toUpdate = getallResourcePool.Copy();
			toUpdate.Name = Randomizer.GetString();

			Sut.Update(toUpdate);

			var result = _getByIdStrategy.Get(toUpdate.ArtifactID);
			result.Should().BeEquivalentTo(toUpdate, o => o.
				Excluding(x => x.Client.ArtifactID));
		}
	}
}
