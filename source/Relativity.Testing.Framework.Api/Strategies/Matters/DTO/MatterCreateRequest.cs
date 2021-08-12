using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterRequest : MatterBaseDtoV1
	{
		public MatterRequest(Matter matter, int statusId)
		{
			Name = matter.Name;
			Number = matter.Number;
			Status = new Securable<Artifact>(new Artifact(statusId));
			Client = new Securable<Artifact>(new Artifact(matter.Client.ArtifactID));
			Keywords = matter.Keywords;
			Notes = matter.Notes;
		}
	}
}
