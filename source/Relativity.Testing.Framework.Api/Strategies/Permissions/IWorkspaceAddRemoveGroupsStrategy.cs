using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of setting the groups to add/remove for workspace.
	/// </summary>
	internal interface IWorkspaceAddRemoveGroupsStrategy
	{
		/// <summary>
		/// Sets the group seletor (added/removed groups) to the specified workspace.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="selector">The group selector.</param>
		void AddRemoveWorkspaceGroups(int workspaceId, GroupSelector selector);
	}
}
