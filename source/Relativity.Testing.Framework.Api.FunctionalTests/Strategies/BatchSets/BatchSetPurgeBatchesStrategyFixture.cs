using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IPurgeBatchesStrategy))]
	internal class BatchSetPurgeBatchesStrategyFixture : ApiServiceTestFixture<IPurgeBatchesStrategy>
	{
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

				batchSet = Facade.Resolve<ICreateBatchSetStrategy>().Create(DefaultWorkspace.ArtifactID, batchModel);
				Facade.Resolve<ICreateBatchesStrategy>().CreateBatches(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);
			});

			var result = Sut.PurgeBatches(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);

			result.Count.Should().BeGreaterThan(0);
			result.Action.Should().Be(BatchProcessAction.Purge.ToString());
		}

		[Test]
		[VersionRange("<12.1")]
		public void PurgeBatches_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.PurgeBatches(DefaultWorkspace.ArtifactID, int.MaxValue));
		}
	}
}
