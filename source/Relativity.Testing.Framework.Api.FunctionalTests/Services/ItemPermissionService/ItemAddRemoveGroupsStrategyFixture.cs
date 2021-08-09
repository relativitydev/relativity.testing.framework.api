using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.FunctionalTests.Services
{
	[Ignore("Refactor when item support will be implemented. By item, we mean ObjectType or Field or FieldCategory.")]
	[TestOf(typeof(IItemPermissionService))]
	internal class ItemAddRemoveGroupsStrategyFixture : ApiServiceTestFixture<IItemPermissionService>
	{
		private int _itemArtifactId;

		private Workspace _workspace;

		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();
			////_workspace = CreateForTest<Workspace>(options: TestEntityOptions.AutoDelete);
			_workspace = new Workspace { ArtifactID = 1017956 };
			_itemArtifactId = 1035255;
		}

		[Test]
		public void Add()
		{
			GroupSelector selector = null;

			Arrange(() =>
			{
				selector = Sut.GetItemGroupSelector(_workspace.ArtifactID, _itemArtifactId);

				if (!selector.DisabledGroups.Any())
				{
					var enabledGroup = selector.EnabledGroups.Last();
					selector.EnabledGroups.RemoveAll(x => x.ArtifactID == enabledGroup.ArtifactID);
					selector.DisabledGroups.Add(enabledGroup);
					Sut.AddRemoveItemGroups(_workspace.ArtifactID, _itemArtifactId, selector);
				}
			});

			var disabledGroup = selector.DisabledGroups.Last();

			selector.DisabledGroups.RemoveAll(x => x.ArtifactID == disabledGroup.ArtifactID);
			selector.EnabledGroups.Add(disabledGroup);

			Sut.AddRemoveItemGroups(_workspace.ArtifactID, _itemArtifactId, selector);

			var result = Sut.GetItemGroupSelector(_workspace.ArtifactID, _itemArtifactId);

			result.Should().NotBeNull();
			result.DisabledGroups.Should().NotContain(disabledGroup);
			result.EnabledGroups.Select(x => x.Name).Should().Contain(disabledGroup.Name);
			result.LastModified.Should().BeAfter(selector.LastModified);
		}

		[Test]
		public void Remove()
		{
			GroupSelector selector = null;

			Arrange(() =>
			{
				selector = Sut.GetItemGroupSelector(_workspace.ArtifactID, _itemArtifactId);

				if (!selector.EnabledGroups.Any())
				{
					var disabledGroup = selector.DisabledGroups.Last();
					selector.DisabledGroups.RemoveAll(x => x.ArtifactID == disabledGroup.ArtifactID);
					selector.EnabledGroups.Add(disabledGroup);
					Sut.AddRemoveItemGroups(_workspace.ArtifactID, _itemArtifactId, selector);
				}
			});

			var enabledGroup = selector.EnabledGroups.Last();

			selector.EnabledGroups.RemoveAll(x => x.ArtifactID == enabledGroup.ArtifactID);
			selector.DisabledGroups.Add(enabledGroup);

			Sut.AddRemoveItemGroups(_workspace.ArtifactID, _itemArtifactId, selector);

			var result = Sut.GetItemGroupSelector(_workspace.ArtifactID, _itemArtifactId);

			result.Should().NotBeNull();
			result.EnabledGroups.Should().NotContain(enabledGroup);
			result.DisabledGroups.Select(x => x.Name).Should().Contain(enabledGroup.Name);
			result.LastModified.Should().BeAfter(selector.LastModified);
		}
	}
}
