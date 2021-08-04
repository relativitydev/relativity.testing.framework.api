using System.Collections.Generic;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IGetWorkspaceGroupUsersStrategy
	{
		Task<List<NamedArtifact>> GetAsync(int workspaceId, int groupId);

		Task<List<NamedArtifact>> GetAsync(int workspaceId, string groupName);
	}
}
