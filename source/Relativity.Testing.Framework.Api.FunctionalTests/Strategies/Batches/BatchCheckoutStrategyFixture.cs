using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[VersionRange(">=12.1")]
	[TestOf(typeof(IBatchCheckoutStrategy))]
	internal class BatchCheckoutStrategyFixture : ApiServiceTestFixture<IBatchCheckoutStrategy>
	{
		[Test]
		public void Checkout_BatchStatusShouldBeInProgress()
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
					BatchPrefix = Randomizer.GetString("Checkout", 20),
					DataSource = new NamedArtifact { ArtifactID = keywordSearch.ArtifactID }
				};

				var batchSet = Facade.Resolve<ICreateBatchSetStrategy>()
					.Create(DefaultWorkspace.ArtifactID, batchModel);

				Facade.Resolve<ICreateBatchesStrategy>().CreateBatches(DefaultWorkspace.ArtifactID, batchSet.ArtifactID);

				batch = Facade.Resolve<IBatchQueryStrategy>().
					Query(DefaultWorkspace.ArtifactID, x => x.BatchSet, batchModel.Name).First();

				user = Facade.Resolve<ICreateStrategy<User>>().Create(new User());

				var group = Facade.Resolve<ICreateStrategy<Group>>().Create(new Group());

				Facade.Resolve<IWorkspacePermissionService>().AddWorkspaceToGroups(DefaultWorkspace.ArtifactID, group.ArtifactID);
				Facade.Resolve<IUserAddToGroupStrategy>().AddToGroup(user.ArtifactID, group.ArtifactID);
			});

			Sut.Checkout(DefaultWorkspace.ArtifactID, batch.ArtifactID, user.ArtifactID);

			var batchAfterAction = Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Batch>>().Get(DefaultWorkspace.ArtifactID, batch.ArtifactID);

			batchAfterAction.BatchStatus.Should().Be("In Progress");
			batchAfterAction.AssignedTo.ArtifactID.Should().Be(user.ArtifactID);
		}
	}
}
