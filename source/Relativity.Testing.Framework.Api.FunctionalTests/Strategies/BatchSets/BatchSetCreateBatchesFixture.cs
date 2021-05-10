using System.Net.Http;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(ICreateBatchesStrategy))]
	internal class BatchSetCreateBatchesFixture : ApiServiceTestFixture<ICreateBatchesStrategy>
	{
		public BatchSetCreateBatchesFixture()
		{
		}

		public BatchSetCreateBatchesFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void CreateBatches()
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
			});

			var result = Sut.CreateBatches(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);

			result.Count.Should().BeGreaterThan(0);
			result.Action.Should().Be(BatchProcessAction.Create.ToString());
		}

		[Test]
		public void CreateBatches_Missing()
		{
			Assert.Throws<HttpRequestException>(() =>
				Sut.CreateBatches(DefaultWorkspace.ArtifactID, int.MaxValue));
		}
	}
}
