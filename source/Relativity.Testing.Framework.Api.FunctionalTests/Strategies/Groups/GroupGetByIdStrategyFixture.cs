using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGroupGetByIdStrategy))]
	internal class GroupGetByIdStrategyFixture : ApiServiceTestFixture<IGroupGetByIdStrategy>
	{
		[Test]
		public void Get_Missing()
		{
			Group result = Sut.Get(int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Group expectedEntity = null;

			Arrange(x => x
				.Create<Group>(3)
					.PickMiddle(out expectedEntity));

			Group result = Sut.Get(expectedEntity.ArtifactID);

			result.Should().BeEquivalentTo(
				expectedEntity,
				o => o.Excluding(x => x.Client)
					.Excluding(x => x.Guids)
					.Excluding(x => x.Meta)
					.Excluding(x => x.Actions)
					.Excluding(x => x.LastModifiedBy)
					.Excluding(x => x.LastModifiedOn)
					.Excluding(x => x.CreatedBy)
					.Excluding(x => x.CreatedOn));
			result.Client.ArtifactID.Should().Be(expectedEntity.Client.ArtifactID);
			result.Client.Name.Should().Be(expectedEntity.Client.Name);
		}
	}
}
