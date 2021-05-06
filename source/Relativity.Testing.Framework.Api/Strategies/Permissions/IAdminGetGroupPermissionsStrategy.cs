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
		/// <returns>An instance of <see cref="GroupPermissions"/>.</returns>
		GroupPermissions Get(int groupId);

		/// <summary>
		/// Gets the admin permissions for a group.
		/// </summary>
		/// <param name="groupName">The group name.</param>
		/// <returns>An instance of <see cref="GroupPermissions"/>.</returns>
		GroupPermissions Get(string groupName);
	}
}
