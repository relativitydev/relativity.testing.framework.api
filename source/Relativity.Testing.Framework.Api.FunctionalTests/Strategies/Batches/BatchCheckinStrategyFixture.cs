using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IBatchCheckinStrategy))]
	internal class BatchCheckinStrategyFixture : ApiServiceTestFixture<IBatchCheckinStrategy>
	{
		[Test]
		public void Checkin_WithIsCompletedAsTrue_BatchStatusShouldBeCompletedAndUserAssignmentShouldNotChange()
		{
			ArrangeEnvironmentForTests(out Batch batch, out User user);

			Sut.Checkin(DefaultWorkspace.ArtifactID, batch.ArtifactID, isCompleted: true);

			var batchAfterAction = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Batch>>().Get(DefaultWorkspace.ArtifactID, batch.ArtifactID);

			batchAfterAction.BatchStatus.Should().Be("Completed");
			batchAfterAction.AssignedTo.ArtifactID.Should().Be(user.ArtifactID);
		}

		[Test]
		public void Checkin_WithIsCompletedAsFalse_BatchStatusShouldBeEmptyAndUserAssignmentShouldNull()
		{
			ArrangeEnvironmentForTests(out Batch batch, out _);

			Sut.Checkin(DefaultWorkspace.ArtifactID, batch.ArtifactID, isCompleted: false);

			var batchAfterAction = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Batch>>().Get(DefaultWorkspace.ArtifactID, batch.ArtifactID);

			batchAfterAction.BatchStatus.Should().Be(string.Empty);
			batchAfterAction.AssignedTo.Should().BeNull();
		}

		private void ArrangeEnvironmentForTests(out Batch batch, out User user)
		{
			Batch localBatch = null;
			User localUser = null;

			Arrange(() =>
			{
				var keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
					.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());

				var batchModel = new BatchSet
				{
					Name = Randomizer.GetString(),
					BatchSize = 2,
					BatchPrefix = Randomizer.GetString("Checkin", 20),
					DataSource = new NamedArtifact { ArtifactID = keywordSearch.ArtifactID }
				};

				var batchSet = Facade.Resolve<ICreateBatchSetStrategy>()
					.Create(DefaultWorkspace.ArtifactID, batchModel);

				Facade.Resolve<ICreateBatchesStrategy>().CreateBatches(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);

				localBatch = Facade.Resolve<IBatchQueryStrategy>().
					Query(DefaultWorkspace.ArtifactID, x => x.BatchSet, batchModel.Name).First();

				localUser = Facade.Resolve<ICreateStrategy<User>>().Create(new User());

				var group = Facade.Resolve<ICreateStrategy<Group>>().Create(new Group());

				Facade.Resolve<IWorkspaceAddToGroupsStrategy>().AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, group.ArtifactID);
				Facade.Resolve<IUserAddToGroupStrategy>().AddToGroup(localUser.ArtifactID, group.ArtifactID);

				Facade.Resolve<IBatchAssignToUserStrategy>().AssignToUser(DefaultWorkspace.ArtifactID, localBatch.ArtifactID, localUser.ArtifactID);
			});

			batch = localBatch;
			user = localUser;
		}
	}
}
