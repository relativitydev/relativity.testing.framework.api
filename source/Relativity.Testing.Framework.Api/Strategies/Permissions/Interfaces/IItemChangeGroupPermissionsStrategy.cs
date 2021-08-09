using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IItemChangeGroupPermissionsStrategy
	{
		Task SetAsync(int workspaceId, int itemId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		Task SetAsync(int workspaceId, int itemId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		Task SetAsync(int workspaceId, int itemId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset);

		Task SetAsync(int workspaceId, int itemId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset);
	}
}
