using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class TabOrderResponsePreOsier : NamedArtifact
	{
		public int ParentArtifactID { get; set; }

		public int Order { get; set; }

		internal Tab ToTab()
		{
			return new Tab
			{
				ArtifactID = ArtifactID,
				Name = Name,
				Parent = new Artifact(ParentArtifactID),
				Order = Order
			};
		}
	}
}
