using System;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of setting the admin permissions for a group using <see cref="GroupPermissionsChangeset"/>.
	/// </summary>
	internal interface IAdminChangeGroupPermissionsStrategy
	{
		/// <summary>
		/// Sets the admin permissions for a group using changeset setter.
		/// </summary>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		void Set(int groupId, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the admin permissions for a group using changeset setter.
		/// </summary>
		/// <param name="groupName">The group Name.</param>
		/// <param name="groupPermissionsChangesetSetter">An action to perform the changes to the group permissions.</param>
		void Set(string groupName, Action<GroupPermissionsChangeset> groupPermissionsChangesetSetter);

		/// <summary>
		/// Sets the admin permissions for a group using changeset.
		/// </summary>
		/// <param name="groupId">The group ID.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		void Set(int groupId, GroupPermissionsChangeset groupPermissionsChangeset);

		/// <summary>
		/// Sets the admin permissions for a group using changeset.
		/// </summary>
		/// <param name="groupName">The group Name.</param>
		/// <param name="groupPermissionsChangeset">The group permissions changeset.</param>
		void Set(string groupName, GroupPermissionsChangeset groupPermissionsChangeset);
	}
}
