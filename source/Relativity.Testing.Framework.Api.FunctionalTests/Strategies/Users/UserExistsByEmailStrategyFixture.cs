using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUserExistsByEmailStrategy))]
	internal class UserExistsByEmailStrategyFixture : ApiServiceTestFixture<IUserExistsByEmailStrategy>
	{
		[Test]
		public void Exists_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Exists(null));
		}

		[Test]
		public void Exists_Missing()
		{
			var result = Sut.Exists(Guid.NewGuid().ToString());

			result.Should().BeFalse();
		}

		[Test]
		public void Exists_Existing()
		{
			User expectedEntity = null;

			Arrange(x => x.Create(out expectedEntity));

			var result = Sut.Exists(expectedEntity.EmailAddress);

			result.Should().BeTrue();
		}
	}
}
