using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IBatchQueryStrategy))]
	internal class BatchesQueryStrategyFixture : ApiServiceTestFixture<IBatchQueryStrategy>
	{
		[Test]
		public void Query_Missing()
		{
			var result = Sut.Query(DefaultWorkspace.ArtifactID, x => x.BatchSet, Randomizer.GetString());

			result.Should().BeEmpty();
		}

		[Test]
		public void Query_Existing()
		{
			BatchSet batchSet = null;

			Arrange(() =>
			{
				var keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
					.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());

				var batchModel = new BatchSet
				{
					Name = Randomizer.GetString(),
					BatchSize = 2,
					BatchPrefix = Randomizer.GetString("Query", 20),
					DataSource = new NamedArtifact { ArtifactID = keywordSearch.ArtifactID }
				};

				batchSet = Facade.Resolve<ICreateBatchSetStrategy>()
					.Create(DefaultWorkspace.ArtifactID, batchModel);

				Facade.Resolve<ICreateBatchesStrategy>().CreateBatches(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);
			});

			var result = Sut.Query(DefaultWorkspace.ArtifactID, x => x.BatchSet, batchSet.Name);
			result.Length.Should().Be(5);

			foreach (var batch in result)
			{
				batch.BatchName.Should().NotBeNullOrWhiteSpace();
				batch.BatchSize.Should().BePositive();
			}
		}
	}
}
