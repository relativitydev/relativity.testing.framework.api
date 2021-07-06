using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GroupDTO
	{
		public GroupDTO(int groupId)
		{
			Group = new Artifact(groupId);
		}

		public Artifact Group { get; set; }
	}
}
