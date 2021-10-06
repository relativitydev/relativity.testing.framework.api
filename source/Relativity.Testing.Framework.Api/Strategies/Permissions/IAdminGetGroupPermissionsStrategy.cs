using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the admin permissions for a group.
	/// </summary>
	internal interface IAdminGetGroupPermissionsStrategy
	{
		/// <summary>
		/// Gets the admin permissions for a group.
		/// </summary>
		/// <param name="groupId">The group ID.</param>
		/// <returns>An instance of [GroupPermissions](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.GroupPermissions.html).</returns>
		GroupPermissions Get(int groupId);

		/// <summary>
		/// Gets the admin permissions for a group.
		/// </summary>
		/// <param name="groupName">The group name.</param>
		/// <returns>An instance of [GroupPermissions](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.GroupPermissions.html).</returns>
		GroupPermissions Get(string groupName);
	}
}
