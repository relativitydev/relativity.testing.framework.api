using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<Tab>))]
	internal class TabGetByIdStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<Tab>>
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
			Tab existingTab = null;

			ArrangeWorkingWorkspace(x => x.Create(new Tab()).Pick(out existingTab));

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingTab.ArtifactID);

			result.Should().BeEquivalentTo(existingTab);
		}
	}
}
