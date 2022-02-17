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
		/// <param name="groupID">Id of the group to which user is being removed.</param>
		/// <param name="userArtifactID">ArtifactId of the user.</param>
		void Wait(int groupID, int userArtifactID);
	}
}
