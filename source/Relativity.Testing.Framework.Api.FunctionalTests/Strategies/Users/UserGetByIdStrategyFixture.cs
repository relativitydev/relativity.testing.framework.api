using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetByIdStrategy<User>))]
	internal class UserGetByIdStrategyFixture : ApiServiceTestFixture<IGetByIdStrategy<User>>
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
			User expectedEntity = null;

			Arrange(x => x
				.Create<User>(3)
					.PickMiddle(out expectedEntity));

			var result = Sut.Get(expectedEntity.ArtifactID);

			result.Should().BeEquivalentTo(
				expectedEntity,
				o => o.Excluding(x => x.Password).Excluding(x => x.Client).Including(x => x.Client.ArtifactID));
		}
	}
}
