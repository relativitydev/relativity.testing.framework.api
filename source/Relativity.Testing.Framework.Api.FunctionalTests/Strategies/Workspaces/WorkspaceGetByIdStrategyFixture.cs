using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(WorkspaceGetByNameStrategy))]
	internal class WorkspaceGetByIdStrategyFixture : ApiServiceTestFixture<IGetByIdStrategy<Workspace>>
	{
		public WorkspaceGetByIdStrategyFixture()
		{
		}

		public WorkspaceGetByIdStrategyFixture(string relativityInstanceAlias)
			: base(relativityInstanceAlias)
		{
		}

		[Test]
		public void Get_Missing()
		{
			var result = Sut.Get(int.MaxValue);

			result.Should().BeNull();
		}

		[Test]
		public void Get_Existing()
		{
			var result = Sut.Get(DefaultWorkspace.ArtifactID);

			result.Should().BeEquivalentTo(DefaultWorkspace);
		}
	}
}
