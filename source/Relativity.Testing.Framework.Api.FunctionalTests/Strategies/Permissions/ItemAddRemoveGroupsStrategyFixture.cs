using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Strategies
{
	[Ignore("Refactor when item support will be implemented. By item, we mean ObjectType or Field or FieldCategory.")]
	[TestOf(typeof(IItemAddRemoveGroupsStrategy))]
	internal class ItemAddRemoveGroupsStrategyFixture : ApiServiceTestFixture<IItemAddRemoveGroupsStrategy>
	{
		private IItemGroupSelectorGetStrategy _getByWorkspaceIdStrategy;
		private int _itemArtifactId;

		private Workspace _workspace;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			////_workspace = CreateForTest<Workspace>(options: TestEntityOptions.AutoDelete);
			_workspace = new Workspace { ArtifactID = 1017956 };
			_itemArtifactId = 1035255;
		}

		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			_getByWorkspaceIdStrategy = Facade.Resolve<IItemGroupSelectorGetStrategy>();
		}

		[Test]
		public async Task Add()
		{
			GroupSelector selector = null;

			Arrange(async () =>
			{
				selector = await _getByWorkspaceIdStrategy.GetAsync(_workspace.ArtifactID, _itemArtifactId).ConfigureAwait(false);

				if (!selector.DisabledGroups.Any())
				{
					var enabledGroup = selector.EnabledGroups.Last();
					selector.EnabledGroups.RemoveAll(x => x.ArtifactID == enabledGroup.ArtifactID);
					selector.DisabledGroups.Add(enabledGroup);
					await Sut.AddRemoveItemGroupsAsync(_workspace.ArtifactID, _itemArtifactId, selector).ConfigureAwait(false);
				}
			});

			var disabledGroup = selector.DisabledGroups.Last();

			selector.DisabledGroups.RemoveAll(x => x.ArtifactID == disabledGroup.ArtifactID);
			selector.EnabledGroups.Add(disabledGroup);

			await Sut.AddRemoveItemGroupsAsync(_workspace.ArtifactID, _itemArtifactId, selector).ConfigureAwait(false);

			var result = await _getByWorkspaceIdStrategy.GetAsync(_workspace.ArtifactID, _itemArtifactId).ConfigureAwait(false);

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
				selector = await _getByWorkspaceIdStrategy.GetAsync(_workspace.ArtifactID, _itemArtifactId).ConfigureAwait(false);

				if (!selector.EnabledGroups.Any())
				{
					var disabledGroup = selector.DisabledGroups.Last();
					selector.DisabledGroups.RemoveAll(x => x.ArtifactID == disabledGroup.ArtifactID);
					selector.EnabledGroups.Add(disabledGroup);
					await Sut.AddRemoveItemGroupsAsync(_workspace.ArtifactID, _itemArtifactId, selector).ConfigureAwait(false);
				}
			});

			var enabledGroup = selector.EnabledGroups.Last();

			selector.EnabledGroups.RemoveAll(x => x.ArtifactID == enabledGroup.ArtifactID);
			selector.DisabledGroups.Add(enabledGroup);

			await Sut.AddRemoveItemGroupsAsync(_workspace.ArtifactID, _itemArtifactId, selector).ConfigureAwait(false);

			var result = await _getByWorkspaceIdStrategy.GetAsync(_workspace.ArtifactID, _itemArtifactId).ConfigureAwait(false);

			result.Should().NotBeNull();
			result.EnabledGroups.Should().NotContain(enabledGroup);
			result.DisabledGroups.Select(x => x.Name).Should().Contain(enabledGroup.Name);
			result.LastModified.Should().BeAfter(selector.LastModified);
		}
	}
}
