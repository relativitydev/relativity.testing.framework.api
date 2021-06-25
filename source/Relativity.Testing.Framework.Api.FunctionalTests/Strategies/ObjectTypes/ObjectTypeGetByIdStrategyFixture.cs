using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<ObjectType>))]
	internal class ObjectTypeGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<ObjectType>>
	{
		[Test]
		public void Get_Missing()
		{
			var exception = Assert.Throws<HttpRequestException>(() => Sut.Get(-1, int.MaxValue));

			Assert.IsTrue(exception.Message.StartsWith("StatusCode: 404, ReasonPhrase: 'Not Found'"));
		}

		[Test]
		public void Get_Existing()
		{
			ObjectType existingObjectType = null;

			Arrange(() =>
			{
				existingObjectType = Facade.Resolve<ICreateWorkspaceEntityStrategy<ObjectType>>()
					.Create(-1, new ObjectType());
			});

			var result = Sut.Get(-1, existingObjectType.ArtifactID);

			result.Should().BeEquivalentTo(existingObjectType);
		}
	}
}
