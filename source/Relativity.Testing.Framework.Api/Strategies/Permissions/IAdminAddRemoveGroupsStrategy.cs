using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of setting the groups to add/remove  to and from admin permissions.
	/// </summary>
	internal interface IAdminAddRemoveGroupsStrategy
	{
		/// <summary>
		/// Sets the group seletor (added/removed groups) to and from admin permissions.
		/// </summary>
		/// <param name="selector">The group selector.</param>
		void AddRemoveGroups(GroupSelector selector);
	}
}
