using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteByIdStrategy<Matter>))]
	internal class MatterDeleteByIdStrategyFixture : ApiServiceTestFixture<IDeleteByIdStrategy<Matter>>
	{
		public MatterDeleteByIdStrategyFixture()
		{
		}

		public MatterDeleteByIdStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Delete_Missing()
		{
			int id = 9_999_999;

			var exception = Assert.Throws<HttpRequestException>(() =>
				Sut.Delete(id));

			exception.Message.Should().StartWith($"Matter ArtifactID {id} is invalid.");
		}

		[Test]
		public void Delete_Existing()
		{
			Matter toDelete = null;

			Arrange(x => x.Create(out toDelete));

			Sut.Delete(toDelete.ArtifactID);

			Facade.Resolve<IGetByIdStrategy<Matter>>().Get(toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
