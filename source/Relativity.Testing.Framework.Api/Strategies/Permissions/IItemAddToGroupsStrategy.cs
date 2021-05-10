namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of adding the item to the group.
	/// </summary>
	internal interface IItemAddToGroupsStrategy
	{
		/// <summary>
		/// Adds the item to the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupNames">The collection of group names.</param>
		void AddItemToGroups(int workspaceId, int itemId, params string[] groupNames);

		/// <summary>
		/// Adds the item to the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupIds">The collection of group IDs.</param>
		void AddItemToGroups(int workspaceId, int itemId, params int[] groupIds);
	}
}
