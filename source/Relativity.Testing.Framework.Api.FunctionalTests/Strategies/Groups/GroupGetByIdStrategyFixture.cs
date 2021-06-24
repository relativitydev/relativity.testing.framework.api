using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(GroupGetByIdStrategy))]
	internal class GroupGetByIdStrategyFixture : ApiServiceTestFixture<IGetByIdStrategy<Group>>
	{
		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Group expectedEntity = null;

			Arrange(x => x
				.Create<Group>(3)
					.PickMiddle(out expectedEntity));

			var result = Sut.Get(expectedEntity.ArtifactID);

			result.Should().BeEquivalentTo(expectedEntity, o => o.Excluding(x => x.Client));
			result.Client.ArtifactID.Should().Be(expectedEntity.Client.ArtifactID);
			result.Client.Name.Should().Be(expectedEntity.Client.Name);
		}
	}
}
