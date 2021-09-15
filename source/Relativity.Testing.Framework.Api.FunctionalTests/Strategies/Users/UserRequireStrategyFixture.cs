using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IRequireWithEnsureNewStrategy<User>))]
	internal class UserRequireStrategyFixture : ApiServiceTestFixture<IRequireWithEnsureNewStrategy<User>>
	{
		[Test]
		public void Require_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Require(null));
		}

		[Test]
		public void Require_Existing()
		{
			User existingUser = null;

			Arrange(x => x.Create(new User())
				.Pick(out existingUser));

			var toUpdate = existingUser.Copy();
			toUpdate.EmailAddress = Randomizer.GetString("AT_{0}@mail.com");
			toUpdate.Password = Randomizer.GetString("AT_{0}");

			var result = Sut.Require(toUpdate);

			result.Client.ArtifactID.Should().Be(toUpdate.Client.ArtifactID);
			result.Should().BeEquivalentTo(
				toUpdate,
				o => o.Excluding(x => x.Client)
					.Excluding(x => x.Password)
					.Excluding(x => x.DocumentSkip)
					.Excluding(x => x.ArtifactID));
		}

		[Test]
		public void Require_Missing()
		{
			var user = new User
			{
				EmailAddress = Randomizer.GetString("AT_{0}@mail.com")
			};

			var result = Sut.Require(user);

			result.ArtifactID.Should().BePositive();
			result.Client.ArtifactID.Should().Be(user.Client.ArtifactID);
			result.EmailAddress.Should().Be(user.EmailAddress);
			result.FirstName.Should().Be(user.FirstName);
			result.LastName.Should().Be(user.LastName);
		}

		[Test]
		public void Require_Existing_EnsureNew_False()
		{
			User existingUser = null;

			Arrange(x => x.Create(new User())
				.Pick(out existingUser));

			var toUpdate = existingUser.Copy();
			toUpdate.EmailAddress = Randomizer.GetString("AT_{0}@mail.com");
			toUpdate.Password = Randomizer.GetString("AT_{0}");

			var result = Sut.Require(toUpdate, false);

			result.Client.ArtifactID.Should().Be(toUpdate.Client.ArtifactID);
			result.EmailAddress.Should().BeEquivalentTo(toUpdate.EmailAddress);
		}

		[Test]
		public void Require_Missing_EnsureNew_False()
		{
			var user = new User
			{
				EmailAddress = Randomizer.GetString("AT_{0}@mail.com")
			};

			var result = Sut.Require(user, false);

			result.ArtifactID.Should().BePositive();
			result.Client.ArtifactID.Should().Be(user.Client.ArtifactID);
			result.EmailAddress.Should().Be(user.EmailAddress);
			result.FirstName.Should().Be(user.FirstName);
			result.LastName.Should().Be(user.LastName);
		}
	}
}
