using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[NonParallelizable] // These tests cause deadlocks in the database when run in parallel.
	[TestOf(typeof(DeleteWorkspaceEntityByIdStrategy<MarkupSet>))]
	internal class MarkupSetDeleteByIdFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<MarkupSet>>
	{
		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<ObjectNotFoundException>(() =>
				Sut.Delete(DefaultWorkspace.ArtifactID, int.MaxValue));
		}

		[Test]
		public void Delete_Existing()
		{
			MarkupSet toDelete = null;

			Arrange(() =>
			{
				toDelete = Facade.Resolve<ICreateWorkspaceEntityStrategy<MarkupSet>>()
					.Create(DefaultWorkspace.ArtifactID, new MarkupSet());
			});

			Sut.Delete(DefaultWorkspace.ArtifactID, toDelete.ArtifactID);

			Facade.Resolve<IGetWorkspaceEntityByIdStrategy<MarkupSet>>().Get(DefaultWorkspace.ArtifactID, toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
