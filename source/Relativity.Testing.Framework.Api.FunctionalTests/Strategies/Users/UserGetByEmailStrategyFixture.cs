using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUserGetByEmailStrategy))]
	public class UserGetByEmailStrategyFixture : ApiTestFixture
	{
		private IUserGetByEmailStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_sut = Facade.Resolve<IUserGetByEmailStrategy>();
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
			User expectedEntity = null;

			Arrange(x => x
				.Create(out Client client)
				.Create(3, new User { Client = client })
					.PickMiddle(out expectedEntity));

			var result = _sut.Get(expectedEntity.EmailAddress);

			result.Should().BeEquivalentTo(
				expectedEntity,
				o => o.Excluding(x => x.Password).Excluding(x => x.Client).Including(x => x.Client.ArtifactID));
		}
	}
}
