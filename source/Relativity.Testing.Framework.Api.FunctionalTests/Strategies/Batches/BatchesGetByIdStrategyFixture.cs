using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<Batch>))]
	internal class BatchesGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<Batch>>
	{
		public BatchesGetByIdStrategyFixture()
		{
		}

		public BatchesGetByIdStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Batch expectedEntity = null;

			Arrange(() =>
			{
				var keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
					.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());

				var batchModel = new BatchSet
				{
					Name = Randomizer.GetString(),
					BatchSize = 2,
					BatchPrefix = Randomizer.GetString("Get", 20),
					DataSource = new NamedArtifact { ArtifactID = keywordSearch.ArtifactID }
				};

				var batchSet = Facade.Resolve<ICreateBatchSetStrategy>()
					.Create(DefaultWorkspace.ArtifactID, batchModel);

				Facade.Resolve<ICreateBatchesStrategy>().CreateBatches(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);

				expectedEntity = Facade.Resolve<IGetAllWorkspaceEntitiesStrategy<Batch>>().
					GetAll(DefaultWorkspace.ArtifactID).First(x => x.BatchSet.Equals(batchSet.Name));
			});

			var result = Sut.Get(DefaultWorkspace.ArtifactID, expectedEntity.ArtifactID);

			result.Should().BeEquivalentTo(expectedEntity);
		}
	}
}
