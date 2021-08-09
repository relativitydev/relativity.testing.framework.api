using Relativity.Testing.Framework.Api.Models;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MatterRequestV1
	{
		public MatterRequestV1(Matter matter, int statusId)
		{
			Name = matter.Name;
			Number = matter.Number;
			Status = new SecuredValue<Artifact>
			{
				Value = new Artifact(statusId)
			};
			Client = new SecuredValue<Artifact>
			{
				Value = new Artifact(matter.Client.ArtifactID)
			};
			Keywords = matter.Keywords;
			Notes = matter.Notes;
		}

		public string Name { get; set; }

		public string Number { get; set; }

		public SecuredValue<Artifact> Status { get; set; }

		public SecuredValue<Artifact> Client { get; set; }

		public string Keywords { get; set; }

		public string Notes { get; set; }
	}
}
