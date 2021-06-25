using System.Net.Http;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ObjectTypeDeleteByIdStrategy))]
	internal class ObjectTypeDeleteStrategyFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<ObjectType>>
	{
		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(-1, int.MaxValue));
		}

		[Test]
		public void Delete_Existing()
		{
			ObjectType toDelete = null;

			Arrange(() =>
			{
				toDelete = Facade.Resolve<ICreateWorkspaceEntityStrategy<ObjectType>>()
					.Create(-1, new ObjectType());
			});

			Sut.Delete(-1, toDelete.ArtifactID);

			var exception = Assert.Throws<HttpRequestException>(() => Facade.Resolve<IGetWorkspaceEntityByIdStrategy<ObjectType>>()
																			.Get(-1, toDelete.ArtifactID));

			Assert.IsTrue(exception.Message.StartsWith("StatusCode: 404, ReasonPhrase: 'Not Found'"));
		}
	}
}
