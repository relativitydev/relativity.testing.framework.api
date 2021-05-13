using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of setting the admin permissions for a group.
	/// </summary>
	internal interface IAdminSetGroupPermissionsStrategy
	{
		/// <summary>
		/// Sets the admin permissions for a group.
		/// </summary>
		/// <param name="groupPermissions">The admin permissions for a group.</param>
		void Set(GroupPermissions groupPermissions);
	}
}
