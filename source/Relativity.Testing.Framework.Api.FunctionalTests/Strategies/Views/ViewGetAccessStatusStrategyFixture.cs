using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[TestOf(typeof(IGetWorkspaceEntityByIdStrategy<ViewAccessStatus>))]
	internal class ViewGetAccessStatusStrategyFixture : ApiServiceTestFixture<IGetWorkspaceEntityByIdStrategy<ViewAccessStatus>>
	{
		public ViewGetAccessStatusStrategyFixture()
		{
		}

		public ViewGetAccessStatusStrategyFixture(string relativityInstanceAlias)
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
			View existingView = null;

			ArrangeWorkingWorkspace(x => x
				.Create(new View
				{
					Fields = new[] { new NamedArtifact { Name = "Control Number" } }
				})
				.Pick(out existingView));

			var result = Sut.Get(DefaultWorkspace.ArtifactID, existingView.ArtifactID);

			result.Exists.Should().BeTrue();
			result.CanView.Should().BeTrue();
			result.CanViewCriteriaFields.Should().BeTrue();
		}
	}
}
