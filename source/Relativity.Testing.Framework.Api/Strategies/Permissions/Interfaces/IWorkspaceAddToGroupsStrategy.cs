using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IWorkspaceAddToGroupsStrategy
	{
		Task AddWorkspaceToGroupsAsync(int workspaceId, params int[] groupIds);

		Task AddWorkspaceToGroupsAsync(int workspaceId, params string[] groupNames);
	}
}
