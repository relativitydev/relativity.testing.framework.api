namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of waiting until installing will be finished.
	/// </summary>
	internal interface ILibraryApplicationWaitUntilInstallFinishedStrategy
	{
		/// <summary>
		/// Waits until aplication will be installed.
		/// </summary>
		/// <param name="applicationId">Application ID to check.</param>
		/// <param name="applicationInstallId">Application install ID to check.</param>
		void WaitUntilInstallFinished(int applicationId, int applicationInstallId);
	}
}
