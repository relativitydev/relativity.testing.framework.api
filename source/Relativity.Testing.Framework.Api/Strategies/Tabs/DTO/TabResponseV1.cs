using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabResponseV1 : TabResponseBase
	{
		public string IconIdentifier { get; set; }

		public NamedArtifact ObjectIdentifier { get; set; }

		public Securable<NamedArtifact> ObjectType { get; set; }

		internal Tab ToTab()
		{
			Tab tab = new Tab
			{
				ArtifactID = ObjectIdentifier.ArtifactID,
				IconIdentifier = string.IsNullOrWhiteSpace(IconIdentifier) ?
					TabIconIdentifier.Unknown :
					(TabIconIdentifier)ChoiceNameToEnumMapper.GetEnumValue(typeof(TabIconIdentifier), IconIdentifier),
				IsDefault = IsDefault,
				IsShownInSidebar = IsShownInSidebar,
				IsVisible = IsVisible,
				Link = Link,
				LinkType = string.IsNullOrWhiteSpace(LinkType) ?
					TabLinkType.Unknown :
					(TabLinkType)ChoiceNameToEnumMapper.GetEnumValue(typeof(TabLinkType), LinkType),
				Name = ObjectIdentifier.Name,
				ObjectType = ObjectType?.Value,
				Order = Order,
				Parent = Parent.Value,
				RelativityApplications = RelativityApplications?.ViewableItems
			};

			return tab;
		}
	}
}
