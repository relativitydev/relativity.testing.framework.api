using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IUpdateBatchSetStrategy))]
	internal class BatchSetUpdateFixture : ApiServiceTestFixture<IUpdateBatchSetStrategy>
	{
		[Test]
		[VersionRange(">=12.1")]
		public void Update_WithFilledExistingEntity_ReturnsUpdatedEntity()
		{
			BatchSet batchSetToUpdate = null;
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

				batchSetToUpdate = Facade.Resolve<ICreateBatchSetStrategy>().Create(DefaultWorkspace.ArtifactID, batchModel);
			});

			batchSetToUpdate.BatchSize = 300;
			batchSetToUpdate.Notes = "Test Note";
			batchSetToUpdate.Keywords = "Test Keywords";

			var result = Sut.Update(DefaultWorkspace.ArtifactID, batchSetToUpdate);

			result.Should().BeEquivalentTo(
				batchSetToUpdate,
				x => x.Excluding(y => y.DataSource.Name)
					.Excluding(y => y.BatchUnitField)
					.Excluding(y => y.FamilyField)
					.Excluding(y => y.ReviewedField)
					.Excluding(y => y.AutoBatchSettings)
					.Excluding(y => y.BatchProcessResult));
		}
	}
}
