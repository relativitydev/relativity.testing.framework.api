using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabRequestBase
	{
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
					? new Artifact { ArtifactID = 62 }
					: new Artifact { ArtifactID = 1003663 };
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
