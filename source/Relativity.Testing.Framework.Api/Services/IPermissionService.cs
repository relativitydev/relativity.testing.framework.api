using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the permission API service.
	/// </summary>
	public interface IPermissionService
	{
		/// <summary>
		/// Gets the admin permission API service.
		/// </summary>
		IAdminPermissionService AdminPermissionService { get; }

		/// <summary>
		/// Gets the workspace permission API service.
		/// </summary>
		IWorkspacePermissionService WorkspacePermissionService { get; }

		/// <summary>
		/// Gets the item permission API service.
		/// </summary>
		IItemPermissionService ItemPermissionService { get; }

		/// <summary>
		/// Gets the users from a group at the admin case level.
		/// </summary>
		/// <param name="groupId">The group id.</param>
		/// <returns>A list of <see cref="NamedArtifact"/>s.</returns>
		List<NamedArtifact> GetAdminGroupUsers(int groupId);

		/// <summary>
		/// Gets the users from a group at the admin case level.
		/// </summary>
		/// <param name="groupName">The group Name.</param>
		/// <returns>A list of <see cref="NamedArtifact"/>s.</returns>
		List<NamedArtifact> GetAdminGroupUsers(string groupName);

		/// <summary>
		/// Gets the users from the group with workspace permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group id.</param>
		/// <returns>A list of <see cref="NamedArtifact"/>s.</returns>
		List<NamedArtifact> GetWorkspaceGroupUsers(int workspaceId, int groupId);

		/// <summary>
		/// Gets the users from the group with workspace permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <returns>A list of <see cref="NamedArtifact"/>s.</returns>
		List<NamedArtifact> GetWorkspaceGroupUsers(int workspaceId, string groupName);
	}
}
