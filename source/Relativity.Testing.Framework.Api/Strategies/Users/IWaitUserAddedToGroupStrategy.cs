namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents wait for add user to group strategy.
	/// </summary>
	internal interface IWaitUserAddedToGroupStrategy
	{
		/// <summary>
		/// Waits for the completion of adding user with specified artifactId to the group
		/// with specified groupId in workspace with specified workspaceId.
		/// </summary>
		/// <param name="workspaceId">Id of the workspace inside of which given group exists.</param>
		/// <param name="groupId">Id of the group to which user is being added.</param>
		/// <param name="userArtifactId">ArtifactId of the user.</param>
		void Wait(int workspaceId, int groupId, int userArtifactId);
	}
}
