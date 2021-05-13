namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of adding the user to the group.
	/// </summary>
	internal interface IUserAddToGroupStrategy
	{
		/// <summary>
		/// Adds the user to the group.
		/// </summary>
		/// <param name="userId">The user ID.</param>
		/// <param name="groupId">The group ID.</param>
		void AddToGroup(int userId, int groupId);
	}
}
