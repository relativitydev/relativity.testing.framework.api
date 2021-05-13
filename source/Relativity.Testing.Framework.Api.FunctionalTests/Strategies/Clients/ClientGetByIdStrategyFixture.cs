using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ClientGetByIdStrategy))]
	public class ClientGetByIdStrategyFixture : ApiTestFixture
	{
		private IGetByIdStrategy<Client> _sut;

		public ClientGetByIdStrategyFixture()
		{
		}

		public ClientGetByIdStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IGetByIdStrategy<Client>>();
		}

		[Test]
		public void Get_Missing()
		{
			var result = _sut.Get(int.MaxValue);

			result.Should().BeNull();
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
