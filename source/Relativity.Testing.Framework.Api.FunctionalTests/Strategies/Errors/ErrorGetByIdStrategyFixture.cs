using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetByIdStrategy<Entity>))]
	internal class ErrorGetByIdStrategyFixture : ApiServiceTestFixture<IGetByIdStrategy<Error>>
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
			Error entity = null;

			Arrange(x => x.Create(new Error { Message = "New error" }).Pick(out entity));

			var result = Sut.Get(entity.ArtifactID);

			result.Should().BeEquivalentTo(entity);
		}
	}
}
