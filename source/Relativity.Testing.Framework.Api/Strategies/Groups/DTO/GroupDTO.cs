using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class GroupDTO
	{
		public GroupDTO(Group group)
		{
			GroupRequest = new GroupRequest(group);
		}

		public GroupRequest GroupRequest { get; set; }
	}
}
