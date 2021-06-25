using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IBatchAssignToUserStrategy))]
	internal class BatchAssignToUserStrategyFixture : ApiServiceTestFixture<IBatchAssignToUserStrategy>
	{
		[Test]
		public void AssignToUser()
		{
			Batch batch = null;

			User user = null;

			Arrange(() =>
			{
				var keywordSearch = Facade.Resolve<ICreateWorkspaceEntityStrategy<KeywordSearch>>()
					.Create(DefaultWorkspace.ArtifactID, new KeywordSearch());

				var batchModel = new BatchSet
				{
					Name = Randomizer.GetString(),
					BatchSize = 2,
					BatchPrefix = Randomizer.GetString("AssignToUser", 20),
					DataSource = new NamedArtifact { ArtifactID = keywordSearch.ArtifactID }
				};

				var batchSet = Facade.Resolve<ICreateBatchSetStrategy>()
					.Create(DefaultWorkspace.ArtifactID, batchModel);

				Facade.Resolve<ICreateBatchesStrategy>().CreateBatches(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);

				batch = Facade.Resolve<IBatchQueryStrategy>().
					Query(DefaultWorkspace.ArtifactID, x => x.BatchSet, batchModel.Name).First();

				user = Facade.Resolve<ICreateStrategy<User>>().Create(new User());

				var group = Facade.Resolve<ICreateStrategy<Group>>().Create(new Group());

				Facade.Resolve<IWorkspaceAddToGroupsStrategy>().AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, group.ArtifactID);
				Facade.Resolve<IUserAddToGroupStrategy>().AddToGroup(user.ArtifactID, group.ArtifactID);
			});

			Sut.AssignToUser(DefaultWorkspace.ArtifactID, batch.ArtifactID, user.ArtifactID);

			var result = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Batch>>().Get(DefaultWorkspace.ArtifactID, batch.ArtifactID);

			result.AssignedTo.ArtifactID.Should().Be(user.ArtifactID);
		}
	}
}
