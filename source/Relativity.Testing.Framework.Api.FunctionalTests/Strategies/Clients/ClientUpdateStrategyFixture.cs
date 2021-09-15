using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateStrategy<Client>))]
	internal class ClientUpdateStrategyFixture : ApiServiceTestFixture<IUpdateStrategy<Client>>
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
			Client existingEntity = null;

			Arrange(x => x.Create(new Client()).Pick(out existingEntity));

			var toUpdate = existingEntity.Copy();
			toUpdate.Notes = Randomizer.GetString("Notes_{0}");
			toUpdate.Name = Randomizer.GetString("Name_{0}");

			var result = Sut.Update(toUpdate);

			result.Notes.Should().Be(toUpdate.Notes);
			result.Name.Should().Be(toUpdate.Name);
		}
	}
}
