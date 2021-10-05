using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the item group permissions.
	/// </summary>
	internal interface IItemGetGroupPermissionsStrategy
	{
		/// <summary>
		/// Gets the item group permissions.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupId">The group ID.</param>
		/// <returns>An instance of [GroupPermissions](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.GroupPermissions.html) or <see langword="null"/>.</returns>
		GroupPermissions Get(int workspaceId, int itemId, int groupId);
	}
}
