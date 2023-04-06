using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabRequestBase
	{
		internal const int _ADMIN_WORKSPACE_BASE_PARENT_TAB_ARTIFACT_ID = 62;
		internal const int _ALL_OTHER_WORKSPACES_BASE_PARENT_TAB_ARTIFACT_ID = 1003663;

		public int ArtifactID { get; set; }

		public string IconIdentifier { get; set; }

		public bool IsDefault { get; set; }

		public bool IsShownInSidebar { get; set; }

		public bool IsVisible { get; set; }

		public string Link { get; set; }

		public int LinkType { get; set; }

		public string Name { get; set; }

		public int Order { get; set; }

		public Securable<Artifact> Parent { get; set; }

		public List<Artifact> RelativityApplications { get; set; }

		internal static Tab SetParent(int workspaceId, Tab entity)
		{
			if (entity.Parent == null)
			{
				entity.Parent = workspaceId == -1
					? new Artifact { ArtifactID = _ADMIN_WORKSPACE_BASE_PARENT_TAB_ARTIFACT_ID }
					: new Artifact { ArtifactID = _ALL_OTHER_WORKSPACES_BASE_PARENT_TAB_ARTIFACT_ID };
			}

			return entity;
		}

		internal static Tab SetObjectType(Tab entity)
		{
			if (entity.LinkType != TabLinkType.Object)
			{
				entity.ObjectType = null;
			}

			return entity;
		}
	}
}
