using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Extensions;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateStrategyWithAsync<Matter>))]
	internal class MatterCreateStrategyFixture : ApiServiceTestFixture<ICreateStrategy<Matter>>
	{
		[Test]
		public void Create_WithEmptyEntity()
		{
			var entity = new Matter();

			Matter result = Sut.Create(entity);

			TestIfMatterIsValid(result);
		}

		[Test]
		public void Create_WithFilledEntity()
		{
			Matter entity = ArrangeMatterWithClient();

			Matter result = Sut.Create(entity.Copy());

			TestIfCreatedMatterIsEquivalentToExpected(entity, result);
		}

		private static void TestIfMatterIsValid(Matter result)
		{
			result.ArtifactID.Should().BePositive();
			result.Name.Should().NotBeNullOrEmpty();
			result.Number.Should().NotBeNullOrEmpty();
			result.Status.Should().Be("Active");
			result.Client.Should().NotBeNull();
			result.Keywords.Should().BeEmpty();
			result.Notes.Should().BeEmpty();
		}

		private Matter ArrangeMatterWithClient()
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
			return entity;
		}

		private static void TestIfCreatedMatterIsEquivalentToExpected(Matter entity, Matter result)
		{
			result.ArtifactID.Should().BePositive();
			result.Should().BeEquivalentTo(entity, o => o.Excluding(x => x.ArtifactID).Excluding(x => x.Client.Status.ArtifactID));
		}
	}
}
