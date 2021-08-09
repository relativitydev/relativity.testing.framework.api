using System.Threading.Tasks;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IItemRemoveFromGroupsStrategy
	{
		Task RemoveItemFromGroupsAsync(int workspaceId, int itemId, params string[] groupNames);

		Task RemoveItemFromGroupsAsync(int workspaceId, int itemId, params int[] groupIds);
	}
}
