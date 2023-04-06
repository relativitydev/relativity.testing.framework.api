using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of setting the group permissions for the workspace using [GroupPermissionsChangeset](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.GroupPermissionsChangeset.html).
	/// </summary>
	internal interface IWorkspaceChangeGroupPermissionsStrategy
	{
		/// <summary>
		/// Sets the group permissions for the workspace using changeset setter.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		void Set(int workspaceId, int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset setter.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		void Set(int workspaceId, string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		void Set(int workspaceId, int groupId, GroupPermissionsChangeset groupPermissionsChangeset);

		/// <summary>
		/// Sets the group permissions for the workspace using changeset.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		void Set(int workspaceId, string groupName, GroupPermissionsChangeset groupPermissionsChangeset);
	}
}
