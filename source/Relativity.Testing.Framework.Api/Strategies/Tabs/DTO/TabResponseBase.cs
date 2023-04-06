using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabResponseBase
	{
		public bool IsDefault { get; set; }

		public bool IsShownInSidebar { get; set; }

		public bool IsVisible { get; set; }

		public string Link { get; set; }

		public string LinkType { get; set; }

		public int Order { get; set; }

		public Securable<NamedArtifact> Parent { get; set; }

		public FieldPropagate RelativityApplications { get; set; }
	}
}
