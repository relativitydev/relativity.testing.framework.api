using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of setting the groups to add/remove for workspace item.
	/// </summary>
	internal interface IItemAddRemoveGroupsStrategy
	{
		/// <summary>
		/// Sets the group seletor (added/removed groups) to the specified workspace item.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="selector">The group selector.</param>
		/// <param name="enableLevelSecurity">The value which indicating whether it should enable level security.
		/// By default true.</param>
		void AddRemoveItemGroups(int workspaceId, int itemId, GroupSelector selector, bool enableLevelSecurity = true);
	}
}
