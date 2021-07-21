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
		public void SetParent_SetsParentTo62IfNullAndWorkspaceIsAdminCase()
		{
			Tab tab = new Tab();

			Tab result = TabRequestBase.SetParent(-1, tab);

			result.Parent.ArtifactID.Should().Be(62);
		}

		[Test]
		public void SetParent_SetsParentTo1003663IfNullAndWorkspaceIsAWorkspace()
		{
			Tab tab = new Tab();

			Tab result = TabRequestBase.SetParent(_WORKSPACE_ID, tab);

			result.Parent.ArtifactID.Should().Be(1003663);
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
