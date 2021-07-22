using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(TabRequestBase))]
	public class TabRequestBaseFixture
	{
		private const int _WORKSPACE_ID = 12345;

		[Test]
		public void SetParent_SetsParentToAdminWorkspaceBaseParentTabArtifactIdIfNullAndWorkspaceIsAdminCase()
		{
			Tab tab = new Tab();

			Tab result = TabRequestBase.SetParent(-1, tab);

			result.Parent.ArtifactID.Should().Be(TabRequestBase._ADMIN_WORKSPACE_BASE_PARENT_TAB_ARTIFACT_ID);
		}

		[Test]
		public void SetParent_SetsParentToWorkspaceBaseParentTabArtifactIdIfNullAndWorkspaceIsAWorkspace()
		{
			Tab tab = new Tab();

			Tab result = TabRequestBase.SetParent(_WORKSPACE_ID, tab);

			result.Parent.ArtifactID.Should().Be(TabRequestBase._ALL_OTHER_WORKSPACES_BASE_PARENT_TAB_ARTIFACT_ID);
		}

		[Test]
		public void SetParent_DoesNotChangeParentIfAlreadySet()
		{
			Tab tab = new Tab
			{
				Parent = new Artifact()
			};

			Tab result = TabRequestBase.SetParent(_WORKSPACE_ID, tab);

			result.Parent.Should().Be(tab.Parent);
		}

		[Test]
		public void SetObjectType_SetsObjectTypeToNullIfLinkTypeIsNotObject()
		{
			Tab tab = new Tab
			{
				ObjectType = new NamedArtifact(),
				LinkType = TabLinkType.Link
			};

			Tab result = TabRequestBase.SetObjectType(tab);

			result.ObjectType.Should().BeNull();
		}

		[Test]
		public void SetObjectType_DoesNotChangeObjectTypeIfLinkTypeIsObject()
		{
			Tab tab = new Tab
			{
				ObjectType = new NamedArtifact(),
				LinkType = TabLinkType.Object
			};

			Tab result = TabRequestBase.SetObjectType(tab);

			result.ObjectType.Should().Be(tab.ObjectType);
		}
	}
}
