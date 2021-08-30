﻿using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(DeleteWorkspaceEntityByIdStrategy<Entity>))]
	internal class EntityDeleteByIdStrategyFixture : ApiServiceTestFixture<IDeleteWorkspaceEntityByIdStrategy<Entity>>
	{
		[Test]
		public void Delete_Missing()
		{
			Assert.Throws<ObjectNotFoundException>(() =>
				Sut.Delete(DefaultWorkspace.ArtifactID, int.MaxValue));
		}

		[Test]
		[VersionRange("<12.2")] // Defect in PrairieSmoke-EA image. Can't run until REL-546511 is merged into a new template.
		public void Delete_Existing()
		{
			Entity toDelete = null;

			Arrange(() =>
			{
				toDelete = Facade.Resolve<ICreateWorkspaceEntityStrategy<Entity>>()
					.Create(DefaultWorkspace.ArtifactID, new Entity());
			});

			Sut.Delete(DefaultWorkspace.ArtifactID, toDelete.ArtifactID);

			Facade.Resolve<IGetWorkspaceEntityByIdStrategy<Entity>>().Get(DefaultWorkspace.ArtifactID, toDelete.ArtifactID).
				Should().BeNull();
		}
	}
}
