namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents wait for remove user to group strategy.
	/// </summary>
	internal interface IWaitUserRemoveFromGroupStrategy
	{
		/// <summary>
		/// Waits for the completion of removing user with specified artifactId from the group.
		/// </summary>
		/// <param name="groupId">Id of the group to which user is being added.</param>
		/// <param name="userArtifactId">ArtifactId of the user.</param>
		void Wait(int groupId, int userArtifactId);
	}
}
