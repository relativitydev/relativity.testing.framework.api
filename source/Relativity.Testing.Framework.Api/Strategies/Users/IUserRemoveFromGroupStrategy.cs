namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of removing the user from the group.
	/// </summary>
	internal interface IUserRemoveFromGroupStrategy
	{
		/// <summary>
		/// Removes the user from the group.
		/// </summary>
		/// <param name="userId">The user ID.</param>
		/// <param name="groupId">The group ID.</param>
		void RemoveFromGroup(int userId, int groupId);
	}
}
