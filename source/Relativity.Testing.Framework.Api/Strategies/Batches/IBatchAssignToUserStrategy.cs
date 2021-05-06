namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of assigning a batch to a user.
	/// </summary>
	internal interface IBatchAssignToUserStrategy
	{
		/// <summary>
		/// Assign a batch to a user.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="batchId">The batch ID.</param>
		/// <param name="userId">The user ID.</param>
		void AssignToUser(int workspaceId, int batchId, int userId);
	}
}
