using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabResponsePrePrairieSmoke : TabResponseBase
	{
		public int ArtifactID { get; set; }

		public string Name { get; set; }

		public NamedArtifact ObjectType { get; set; }

		internal Tab ToTab()
		{
			Tab tab = new Tab
			{
				ArtifactID = ArtifactID,
				IsDefault = IsDefault,
				IsShownInSidebar = IsShownInSidebar,
				IsVisible = IsVisible,
				Link = Link,
				LinkType = (TabLinkType)ChoiceNameToEnumMapper.GetEnumValue(typeof(TabLinkType), LinkType),
				Name = Name,
				ObjectType = ObjectType,
				Order = Order,
				Parent = Parent.Value,
				RelativityApplications = RelativityApplications?.ViewableItems
			};

			return tab;
		}
	}
}
