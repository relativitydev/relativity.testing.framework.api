using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateStrategy<Matter>))]
	internal class MatterCreateStrategyFixture : ApiServiceTestFixture<ICreateStrategy<Matter>>
	{
		[Test]
		public void Create_WithNull()
		{
			Assert.Throws<ArgumentNullException>(() =>
				Sut.Create(null));
		}

		[Test]
		public void Create_WithEmptyEntity()
		{
			var entity = new Matter();

			var result = Sut.Create(entity);

			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.Number.Should().NotBeNullOrEmpty();
			result.Status.Should().Be("Active");
			result.Client.Should().NotBeNull();
			result.Keywords.Should().BeEmpty();
			result.Notes.Should().BeEmpty();
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			Client client = null;

			Arrange(x => x.Create(out client));

			var entity = new Matter
			{
				Name = Randomizer.GetString("AT_"),
				Number = Randomizer.GetString(),
				Status = "Inactive",
				Client = client,
				Keywords = Randomizer.GetString(),
				Notes = Randomizer.GetString()
			};

			var result = Sut.Create(entity.Copy());

			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID).Excluding(x => x.Client.Status.ArtifactID));
		}
	}
}
