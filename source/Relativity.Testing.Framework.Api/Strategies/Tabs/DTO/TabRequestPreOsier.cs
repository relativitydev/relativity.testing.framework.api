using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabRequestPreOsier : TabRequestBase
	{
		public NamedArtifact ObjectType { get; set; }

		internal static TabRequestPreOsier FromTab(int workspaceId, Tab entity)
		{
			entity = SetParent(workspaceId, entity);

			entity = SetObjectType(entity);

			TabRequestPreOsier tab = new TabRequestPreOsier
			{
				ArtifactID = entity.ArtifactID,
				IconIdentifier = ChoiceNameToEnumMapper.GetName(entity.IconIdentifier),
				IsDefault = entity.IsDefault,
				IsShownInSidebar = entity.IsShownInSidebar,
				IsVisible = entity.IsVisible,
				Link = entity.Link,
				LinkType = (int)entity.LinkType,
				Name = entity.Name,
				ObjectType = entity.ObjectType,
				Order = entity.Order,
				Parent = entity.Parent,
				RelativityApplications = entity.RelativityApplications
			};

			return tab;
		}
	}
}
