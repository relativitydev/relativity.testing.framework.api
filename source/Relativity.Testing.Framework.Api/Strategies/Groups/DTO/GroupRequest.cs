using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GroupRequest
	{
		public GroupRequest(Group group)
		{
			Name = group.Name;
			Client = new Securable<Artifact>(group.Client);
			Keywords = group.Keywords;
			Notes = group.Notes;
		}

		public string Name { get; set; }

		public Securable<Artifact> Client { get; set; }

		public string Keywords { get; set; }

		public string Notes { get; set; }
	}
}
