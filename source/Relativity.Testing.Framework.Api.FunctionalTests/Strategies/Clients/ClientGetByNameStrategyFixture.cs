using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ClientGetByNameStrategy))]
	public class ClientGetByNameStrategyFixture : ApiTestFixture
	{
		private IGetByNameStrategy<Client> _sut;

		public ClientGetByNameStrategyFixture()
		{
		}

		public ClientGetByNameStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IGetByNameStrategy<Client>>();
		}

		[Test]
		public void Get_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				_sut.Get(null));
		}

		[Test]
		public void Get_Missing()
		{
			var result = _sut.Get(Guid.NewGuid().ToString());

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Client expectedEntity = null;

			Arrange(x => x
				.Create<Client>(3)
					.PickMiddle(out expectedEntity));

			var result = _sut.Get(expectedEntity.Name);

			result.Status.Name.Should().Be(expectedEntity.Status.Name);
			result.Should().BeEquivalentTo(expectedEntity, o => o.Excluding(x => x.Status));
		}
	}
}
