using System;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IDeleteBatchSetStrategy))]
	internal class BatchSetDeleteStrategyFixture : ApiServiceTestFixture<IDeleteBatchSetStrategy>
	{
		[Test]
		[VersionRange(">=12.1")]
		public void Delete_WithValidWorkspaceIdAndBatchSetId_DeletesBatchSet()
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
			});

			Sut.Delete(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);

			var result = Assert.Throws<ArgumentException>(() =>
				Facade.Resolve<IGetBatchSetByIdStrategy>().Get(DefaultWorkspace.ArtifactID, batchSet.ArtifactID));

			result.Message.Should().Contain($"The batch set with ID: {batchSet.ArtifactID} does not exists.");
		}

		[Test]
		[VersionRange("<12.1")]
		public void Delete_ForUnsupportedVersions_ThrowsArgumentException()
		{
			var result = Assert.Throws<ArgumentException>(() =>
				Sut.Delete(DefaultWorkspace.ArtifactID, 1));

			result.Message.Should().Contain("The method Delete does not support version of Relativity lower than 12.1.");
		}
	}
}
