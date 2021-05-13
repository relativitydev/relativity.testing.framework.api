namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of setting the level security for the item.
	/// </summary>
	internal interface IItemLevelSecuritySetStrategy
	{
		/// <summary>
		/// Sets the level security for the item.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item id.</param>
		/// <param name="isEnabled">The value which indicating whether edit this item turned on.</param>
		void Set(int workspaceId, int itemId, bool isEnabled);
	}
}
