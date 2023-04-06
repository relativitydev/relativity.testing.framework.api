using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetDependencyListForWorkspaceEntityStrategy<ObjectType>))]
	internal class ObjectTypeGetDependencyListStrategyFixture : ApiServiceTestFixture<IGetDependencyListForWorkspaceEntityStrategy<ObjectType>>
	{
		[Test]
		public void GetDependencies()
		{
			ObjectType existingObjectType = null;

			ArrangeWorkingWorkspace(x => x
				.Create(out existingObjectType));

			var result = Sut.GetDependencies(DefaultWorkspace.ArtifactID, existingObjectType.ArtifactID);

			result.Should().NotBeNullOrEmpty();
		}
	}
}
