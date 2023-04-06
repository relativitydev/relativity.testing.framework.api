using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<Entity>))]
	internal class EntityGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<Entity>>
	{
		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			Entity entity = null;

			Arrange(() =>
			{
				entity = Facade.Resolve<ICreateWorkspaceEntityStrategy<Entity>>()
					.Create(DefaultWorkspace.ArtifactID, new Entity());
			});

			var result = Sut.Get(DefaultWorkspace.ArtifactID, entity.ArtifactID);

			result.Should().BeEquivalentTo(entity);
		}
	}
}
