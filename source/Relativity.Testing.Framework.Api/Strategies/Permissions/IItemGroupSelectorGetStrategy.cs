using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the item group selector by workspace and item ID.
	/// </summary>
	internal interface IItemGroupSelectorGetStrategy
	{
		/// <summary>
		/// Gets the item group selector by workspace and item ID.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="itemId">The item ID.</param>
		/// <returns>The item group selector.</returns>
		GroupSelector Get(int workspaceId, int itemId);
	}
}
