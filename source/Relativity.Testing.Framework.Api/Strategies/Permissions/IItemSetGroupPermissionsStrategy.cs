using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of setting the item group permissions.
	/// </summary>
	internal interface IItemSetGroupPermissionsStrategy
	{
		/// <summary>
		/// Sets the item group permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupPermissions">The group permissions.</param>
		void Set(int workspaceId, int itemId, GroupPermissions groupPermissions);
	}
}
