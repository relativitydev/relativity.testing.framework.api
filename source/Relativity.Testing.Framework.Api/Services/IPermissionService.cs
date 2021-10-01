using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the permission API service.
	/// </summary>
	/// <example>
	/// <code>
	/// IPermissionService _permissionService = relativityFacade.Resolve&lt;IPermissionService&gt;();
	/// </code>
	/// </example>
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
		/// <example>
		/// <code>
		/// int groupID = 654321;
		///
		/// List&lt;NamedArtifact&gt; adminUsers = _permissionService.GetAdminGroupUsers(groupID);
		/// </code>
		/// </example>
		List<NamedArtifact> GetAdminGroupUsers(int groupId);

		/// <summary>
		/// Gets the users from a group at the admin case level.
		/// </summary>
		/// <param name="groupName">The group Name.</param>
		/// <returns>A list of <see cref="NamedArtifact"/>s.</returns>
		/// <example>
		/// <code>
		/// string groupName = "Group name;
		///
		/// List&lt;NamedArtifact&gt; adminUsers = _permissionService.GetAdminGroupUsers(groupName);
		/// </code>
		/// </example>
		List<NamedArtifact> GetAdminGroupUsers(string groupName);

		/// <summary>
		/// Gets the users from the group with workspace permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group id.</param>
		/// <returns>A list of <see cref="NamedArtifact"/>s.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// int groupID = 654321;
		///
		/// List&lt;NamedArtifact&gt; workspaceUsers = _permissionService.GetWorkspaceGroupUsers(workspaceID, groupID);
		/// </code>
		/// </example>
		List<NamedArtifact> GetWorkspaceGroupUsers(int workspaceId, int groupId);

		/// <summary>
		/// Gets the users from the group with workspace permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <returns>A list of <see cref="NamedArtifact"/>s.</returns>
		/// <example>
		/// <code>
		/// int workspaceID = 123456;
		/// string groupName = "Group name;
		///
		/// List&lt;NamedArtifact&gt; workspaceUsers = _permissionService.GetWorkspaceGroupUsers(workspaceID, groupName);
		/// </code>
		/// </example>
		List<NamedArtifact> GetWorkspaceGroupUsers(int workspaceId, string groupName);
	}
}
