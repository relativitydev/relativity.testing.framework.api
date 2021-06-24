using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateStrategy<User>))]
	internal class UserUpdateStrategyFixture : ApiServiceTestFixture<IUpdateStrategy<User>>
	{
		[Test]
		public void Update_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Update(null));
		}

		[Test]
		public void Update()
		{
			User existingEntity = null;

			Arrange(x => x.Create(new User()).Pick(out existingEntity));

			var toUpdate = existingEntity.Copy();
			toUpdate.EmailAddress = Randomizer.GetString("AT_{0}@mail.com");
			toUpdate.Password = null;

			Sut.Update(toUpdate);

			var result = Facade.Resolve<IGetByIdStrategy<User>>().Get(toUpdate.ArtifactID);

			result.Client.ArtifactID.Should().Be(toUpdate.Client.ArtifactID);
			result.EmailAddress.Should().BeEquivalentTo(toUpdate.EmailAddress);
		}
	}
}
