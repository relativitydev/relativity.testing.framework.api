using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GroupResponse : NamedArtifactWithGuids
	{
		public Securable<NamedArtifactWithGuids> Client { get; set; }

		public GroupType Type { get; set; } = GroupType.SystemGroup;

		public List<HttpAction> Actions { get; set; }

		public Meta Meta { get; set; }

		public string Keywords { get; set; }

		public string Notes { get; set; }
	}
}
