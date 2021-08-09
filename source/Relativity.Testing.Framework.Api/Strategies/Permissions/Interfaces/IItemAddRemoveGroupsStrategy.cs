using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IItemAddRemoveGroupsStrategy
	{
		Task AddRemoveItemGroupsAsync(int workspaceId, int itemId, GroupSelector selector, bool enableLevelSecurity = true);
	}
}
