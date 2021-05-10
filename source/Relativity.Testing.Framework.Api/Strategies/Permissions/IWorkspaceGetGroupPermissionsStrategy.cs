using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the group permissions for the workspace.
	/// </summary>
	internal interface IWorkspaceGetGroupPermissionsStrategy
	{
		/// <summary>
		/// Gets the group permissions for the workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/> or <see langword="null"/> if the workspace is not added to the group.</returns>
		GroupPermissions Get(int workspaceId, int groupId);

		/// <summary>
		/// Gets the group permissions for the workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupName">The group Name.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/> or <see langword="null"/> if the workspace is not added to the group.</returns>
		GroupPermissions Get(int workspaceId, string groupName);
	}
}
