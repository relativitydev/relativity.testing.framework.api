using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<BatchSet>))]
	internal class BatchSetGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<BatchSet>>
	{
		public BatchSetGetByIdStrategyFixture()
		{
		}

		public BatchSetGetByIdStrategyFixture(string relativityInstanceAlias)
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
			BatchSet batchSet = null;

			Arrange(() =>
			{
				var keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>().Create(DefaultWorkspace.ArtifactID, new KeywordSearch());

				var batchModel = new BatchSet
				{
					Name = Randomizer.GetString(),
					BatchSize = 1500,
					BatchPrefix = Randomizer.GetString("BS", 3),
					DataSource = new NamedArtifact { ArtifactID = keywordSearch.ArtifactID }
				};

				batchSet = Facade.Resolve<ICreateWorkspaceEntityStrategy<BatchSet>>().Create(DefaultWorkspace.ArtifactID, batchModel);
			});

			var result = Sut.Get(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);
			result.Should().BeEquivalentTo(batchSet);
		}
	}
}
