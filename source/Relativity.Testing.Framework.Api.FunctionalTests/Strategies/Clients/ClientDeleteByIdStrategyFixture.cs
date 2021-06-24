using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ClientDeleteByIdStrategy))]
	public class ClientDeleteByIdStrategyFixture : ApiTestFixture
	{
		private IDeleteByIdStrategy<Client> _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IDeleteByIdStrategy<Client>>();
		}

		[Test]
		public void Delete_Missing()
		{
			int id = 9_999_999;

			var exception = Assert.Throws<HttpRequestException>(() =>
				_sut.Delete(id));

			exception.Message.Should().StartWith($"Client ArtifactID {id} is invalid.");
		}

		[Test]
		public void Delete_Existing()
		{
			Client toDelete = null;

			Arrange(x => x.Create(out toDelete));

			_sut.Delete(toDelete.ArtifactID);

			Facade.Resolve<IGetByIdStrategy<Client>>().Get(toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
