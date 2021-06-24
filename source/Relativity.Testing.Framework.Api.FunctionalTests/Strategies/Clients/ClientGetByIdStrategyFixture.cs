using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ClientGetByIdStrategyPrePrairieSmoke))]
	public class ClientGetByIdStrategyFixture : ApiTestFixture
	{
		private IGetByIdStrategy<Client> _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IGetByIdStrategy<Client>>();
		}

		[Test]
		[VersionRange("<12.1")]
		public void Get_Missing_PrePrairieSmoke()
		{
			var result = _sut.Get(int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		[VersionRange(">=12.1")]
		public void Get_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				_sut.Get(int.MaxValue));
		}

		[Test]
		public void Get_Existing()
		{
			Client expectedEntity = null;

			Arrange(x => x
				.Create<Client>(3)
					.PickMiddle(out expectedEntity));

			var result = _sut.Get(expectedEntity.ArtifactID);

			result.Status.Name.Should().Be(expectedEntity.Status.Name);
			result.Should().BeEquivalentTo(expectedEntity, o => o.Excluding(x => x.Status));
		}
	}
}
