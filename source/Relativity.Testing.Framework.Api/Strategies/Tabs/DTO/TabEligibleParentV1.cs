using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies.Tabs.DTO
{
	public class TabEligibleParentV1
	{
		public NamedArtifactWithGuids ObjectIdentifier { get; set; }

		public List<TabLinkType> SupportedChildTypeLinkTypes { get; set; }
	}
}
