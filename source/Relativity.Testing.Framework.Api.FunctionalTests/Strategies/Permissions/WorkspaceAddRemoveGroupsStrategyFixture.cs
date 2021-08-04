using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[NonParallelizable]
	[TestOf(typeof(IWorkspaceAddRemoveGroupsStrategy))]
	internal class WorkspaceAddRemoveGroupsStrategyFixture : ApiServiceTestFixture<IWorkspaceAddRemoveGroupsStrategy>
	{
		private IGetByWorkspaceIdStrategy<GroupSelector> _getByWorkspaceIdStrategy;

		private Workspace _workspace;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			Arrange(x => x.Create(out _workspace));
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();

			_getByWorkspaceIdStrategy = Facade.Resolve<IGetByWorkspaceIdStrategy<GroupSelector>>();
		}

		[Test]
		public async Task Add()
		{
			GroupSelector selector = null;

			Arrange(() =>
			{
				selector = _getByWorkspaceIdStrategy.Get(_workspace.ArtifactID);
			});

			var disabledGroup = selector.DisabledGroups.Last();

			selector.DisabledGroups.RemoveAll(x => x.ArtifactID == disabledGroup.ArtifactID);
			selector.EnabledGroups.Add(disabledGroup);

			await Sut.AddRemoveWorkspaceGroupsAsync(_workspace.ArtifactID, selector).ConfigureAwait(false);

			var result = _getByWorkspaceIdStrategy.Get(_workspace.ArtifactID);

			result.Should().NotBeNull();
			result.DisabledGroups.Should().NotContain(disabledGroup);
			result.EnabledGroups.Select(x => x.Name).Should().Contain(disabledGroup.Name);
			result.LastModified.Should().BeAfter(selector.LastModified);
		}

		[Test]
		public async Task Remove()
		{
			GroupSelector selector = null;

			Arrange(async () =>
			{
				selector = _getByWorkspaceIdStrategy.Get(_workspace.ArtifactID);

				if (!selector.EnabledGroups.Any())
				{
					var disabledGroup = selector.DisabledGroups.Last();
					selector.DisabledGroups.RemoveAll(x => x.ArtifactID == disabledGroup.ArtifactID);
					selector.EnabledGroups.Add(disabledGroup);
					await Sut.AddRemoveWorkspaceGroupsAsync(_workspace.ArtifactID, selector).ConfigureAwait(false);
				}
			});

			var enabledGroup = selector.DisabledGroups.Last();

			selector.EnabledGroups.RemoveAll(x => x.ArtifactID == enabledGroup.ArtifactID);
			selector.DisabledGroups.Add(enabledGroup);

			await Sut.AddRemoveWorkspaceGroupsAsync(_workspace.ArtifactID, selector).ConfigureAwait(false);

			var result = _getByWorkspaceIdStrategy.Get(_workspace.ArtifactID);

			result.Should().NotBeNull();
			result.EnabledGroups.Should().NotContain(enabledGroup);
			result.DisabledGroups.Select(x => x.Name).Should().Contain(enabledGroup.Name);
			result.LastModified.Should().BeAfter(selector.LastModified);
		}
	}
}
