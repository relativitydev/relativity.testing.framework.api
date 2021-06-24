using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IMatterGetByNameAndClientIdStrategy))]
	internal class MatterGetByNameAndClientIdStrategyFixture : ApiServiceTestFixture<IMatterGetByNameAndClientIdStrategy>
	{
		[Test]
		public void Get_WithNullName()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Get(null, 1));
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(Randomizer.GetString(), int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Matter expectedEntity = null;

			Arrange(x => x
				.Create(out Client client)
				.Create(3, new Matter { Client = client })
					.PickMiddle(out expectedEntity));

			var result = Sut.Get(expectedEntity.Name, expectedEntity.Client.ArtifactID);

			result.Should().BeEquivalentTo(
				expectedEntity,
				o => o.Excluding(x => x.Client).Including(x => x.Client.ArtifactID).Including(x => x.Client.Name));
		}
	}
}
