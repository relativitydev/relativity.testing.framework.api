using System;
using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface IAdminChangeGroupPermissionsStrategy
	{
		Task SetAsync(int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		Task SetAsync(string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		Task SetAsync(int groupId, GroupPermissionsChangeset groupPermissionsChangeset);

		Task SetAsync(string groupName, GroupPermissionsChangeset groupPermissionsChangeset);
	}
}
