using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetByIdStrategy<Matter>))]
	internal class MatterGetByIdStrategyFixture : ApiServiceTestFixture<IGetByIdStrategy<Matter>>
	{
		[Test]
		public void Get_Missing()
		{
			Matter result = Sut.Get(int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Matter expectedEntity = null;

			Arrange(x => x
				.Create(out Client client)
				.Create(3, new Matter { Client = client })
					.PickMiddle(out expectedEntity));

			Matter result = Sut.Get(expectedEntity.ArtifactID);

			result.Should().BeEquivalentTo(
				expectedEntity,
				o => o.Excluding(x => x.Client).Including(x => x.Client.ArtifactID).Including(x => x.Client.Name));
		}
	}
}
