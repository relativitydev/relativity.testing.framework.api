using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetAllStrategy<Error>))]
	internal class ErrorGetAllStrategyFixture : ApiServiceTestFixture<IGetAllStrategy<Error>>
	{
		[Test]
		public void GetAll()
		{
			Error entity = null;

			Arrange(x => x.Create(new Error { Message = "New error" }).Pick(out entity));

			var result = Sut.GetAll();

			result.Should().Contain(x => x.ArtifactID == entity.ArtifactID);
		}
	}
}
