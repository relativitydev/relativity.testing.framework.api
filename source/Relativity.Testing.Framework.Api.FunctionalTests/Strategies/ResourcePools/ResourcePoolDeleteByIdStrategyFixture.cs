using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteByIdStrategy<ResourcePool>))]
	[Ignore("It looks like these tests are causing other tests to have trouble getting the correct resource pool, needs to be investigated. https://jira.kcura.com/browse/RTF-753")]
	internal class ResourcePoolDeleteByIdStrategyFixture : ApiServiceTestFixture<IDeleteByIdStrategy<ResourcePool>>
	{
		public ResourcePoolDeleteByIdStrategyFixture()
		{
		}

		public ResourcePoolDeleteByIdStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Delete_Missing()
		{
			if (Facade.Resolve<IVersionRangeMatchService>().IsVersionInRange(Facade.RelativityInstanceVersion, "<= 11.2"))
			{
				Assert.Inconclusive();
			}

			Assert.Throws<ObjectNotFoundException>(() =>
				Sut.Delete(int.MaxValue));
		}

		[Test]
		public void Delete_Existing()
		{
			if (Facade.Resolve<IVersionRangeMatchService>().IsVersionInRange(Facade.RelativityInstanceVersion, "<= 11.2"))
			{
				Assert.Inconclusive();
			}

			ResourcePool toDelete = null;

			Arrange(x => x
				.Create(out Client client)
				.Create(new ResourcePool { Name = Randomizer.GetString(), Client = client })
					.Pick(out toDelete));

			Sut.Delete(toDelete.ArtifactID);

			Facade.Resolve<IGetByIdStrategy<ResourcePool>>().Get(toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
