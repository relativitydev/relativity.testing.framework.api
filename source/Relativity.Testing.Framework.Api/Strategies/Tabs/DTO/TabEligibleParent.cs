using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	public class TabEligibleParent
	{
		public NamedArtifactWithGuids ObjectIdentifier { get; set; }

		public List<TabLinkType> SupportedChildTypeLinkTypes { get; set; }
	}
}
