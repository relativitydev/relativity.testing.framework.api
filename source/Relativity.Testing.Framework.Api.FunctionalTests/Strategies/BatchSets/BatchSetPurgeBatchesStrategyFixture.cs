using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IPurgeBatchesStrategy))]
	internal class BatchSetPurgeBatchesStrategyFixture : ApiServiceTestFixture<IPurgeBatchesStrategy>
	{
		public BatchSetPurgeBatchesStrategyFixture()
		{
		}

		public BatchSetPurgeBatchesStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void PurgeBatches()
		{
			BatchSet batchSet = null;

			Arrange(() =>
			{
				var keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>().Create(DefaultWorkspace.ArtifactID, new KeywordSearch());

				var batchModel = new BatchSet
				{
					Name = Randomizer.GetString(),
					BatchSize = 1500,
					BatchPrefix = Randomizer.GetString("CB", 3),
					DataSource = new NamedArtifact { ArtifactID = keywordSearch.ArtifactID }
				};

				batchSet = Facade.Resolve<ICreateWorkspaceEntityStrategy<BatchSet>>().Create(DefaultWorkspace.ArtifactID, batchModel);
				Facade.Resolve<ICreateBatchesStrategy>().CreateBatches(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);
			});

			var result = Sut.PurgeBatches(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);

			result.Count.Should().BeGreaterThan(0);
			result.Action.Should().Be(BatchProcessAction.Purge.ToString());
		}

		[Test]
		public void PurgeBatches_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.PurgeBatches(DefaultWorkspace.ArtifactID, int.MaxValue));
		}
	}
}
