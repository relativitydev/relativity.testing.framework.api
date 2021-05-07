namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of removing the item from the groups.
	/// </summary>
	internal interface IItemRemoveFromGroupsStrategy
	{
		/// <summary>
		/// Removeds the item from the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupNames">The collection of group Names.</param>
		void RemoveItemFromGroups(int workspaceId, int itemId, params string[] groupNames);

		/// <summary>
		/// Removeds the item from the groups.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <param name="groupIds">The collection of group IDs.</param>
		void RemoveItemFromGroups(int workspaceId, int itemId, params int[] groupIds);
	}
}
