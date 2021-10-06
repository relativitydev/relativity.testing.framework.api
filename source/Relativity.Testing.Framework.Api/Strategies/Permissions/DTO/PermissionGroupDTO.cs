using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class PermissionGroupDTO
	{
		public PermissionGroupDTO(int groupId)
		{
			Group = new Artifact(groupId);
		}

		public Artifact Group { get; set; }
	}
}
