using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterRequestPreOsier
	{
		public MatterRequestPreOsier(Matter matter, int statusId)
		{
			Name = matter.Name;
			Number = matter.Number;
			Status = new Artifact(statusId);
			Client = new Artifact(matter.Client.ArtifactID);
			Keywords = matter.Keywords;
			Notes = matter.Notes;
		}

		public string Name { get; set; }

		public string Number { get; set; }

		public Artifact Status { get; set; }

		public Artifact Client { get; set; }

		public string Keywords { get; set; }

		public string Notes { get; set; }
	}
}
