namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents a strategy that waitsfor the user to actually be gone after it has been deleted.
	/// </summary>
	internal interface IWaitUserDeletedStrategy
	{
		/// <summary>
		/// Waits for the user to be gone from Relativity after being deleted.
		/// </summary>
		/// <param name="artifactId">ArtifactId of the user.</param>
		void Wait(int artifactId);

		/// <summary>
		/// Waits for the user to be gone from Relativity after being deleted.
		/// </summary>
		/// <param name="email">The email address of the user.</param>
		void Wait(string email);
	}
}
