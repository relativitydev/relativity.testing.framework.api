using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabRequestV1 : TabRequestBase
	{
		public Securable<NamedArtifact> ObjectType { get; set; }

		internal static TabRequestV1 FromTab(int workspaceId, Tab entity)
		{
			entity = SetParent(workspaceId, entity);

			entity = SetObjectType(entity);

			TabRequestV1 tab = new TabRequestV1
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
