using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IItemAddToGroupsStrategy
	{
		Task AddItemToGroupsAsync(int workspaceId, int itemId, params string[] groupNames);

		Task AddItemToGroupsAsync(int workspaceId, int itemId, params int[] groupIds);
	}
}
