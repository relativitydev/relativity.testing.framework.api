using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetAllWorkspaceEntitiesStrategy<Entity>))]
	internal class EntityGetAllStrategyFixture : ApiServiceTestFixture<IGetAllWorkspaceEntitiesStrategy<Entity>>
	{
		[Test]
		public void GetAll_Existing()
		{
			Arrange(() =>
			{
				Facade.Resolve<ICreateWorkspaceEntityStrategy<Entity>>()
					.Create(DefaultWorkspace.ArtifactID, new Entity());
			});

			var result = Sut.GetAll(DefaultWorkspace.ArtifactID);

			result.Length.Should().BePositive();

			var entity = result[0];

			entity.ArtifactID.Should().BePositive();
			entity.FullName.Should().NotBeNullOrWhiteSpace();
		}
	}
}
