using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[NonParallelizable] // These tests cause deadlocks in the database when run in parallel.
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<MarkupSet>))]
	internal class MarkupSetGetByIdFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<MarkupSet>>
	{
		public MarkupSetGetByIdFixture()
		{
		}

		public MarkupSetGetByIdFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID, int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			MarkupSet existingMarkupSet = null;

			Arrange(() =>
			{
				existingMarkupSet = Facade.Resolve<ICreateWorkspaceEntityStrategy<MarkupSet>>()
					.Create(DefaultWorkspace.ArtifactID, new MarkupSet());
			});

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingMarkupSet.ArtifactID);

			result.Should().BeEquivalentTo(existingMarkupSet);
		}
	}
}
