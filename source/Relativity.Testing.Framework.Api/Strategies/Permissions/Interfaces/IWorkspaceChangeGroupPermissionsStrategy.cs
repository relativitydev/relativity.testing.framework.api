using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IWorkspaceChangeGroupPermissionsStrategy
	{
		Task SetAsync(int workspaceId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		Task SetAsync(int workspaceId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		Task SetAsync(int workspaceId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset);

		Task SetAsync(int workspaceId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset);
	}
}
