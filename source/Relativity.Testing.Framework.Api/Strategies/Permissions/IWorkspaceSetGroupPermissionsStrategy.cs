using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of setting the group permissions for the workspace.
	/// </summary>
	internal interface IWorkspaceSetGroupPermissionsStrategy
	{
		/// <summary>
		/// Sets the group permissions for the workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="groupPermissions">The group permissions.</param>
		void Set(int workspaceId, GroupPermissions groupPermissions);
	}
}
