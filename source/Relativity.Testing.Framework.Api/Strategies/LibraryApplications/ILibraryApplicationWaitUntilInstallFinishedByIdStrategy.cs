namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of waiting until installing will be finished.
	/// </summary>
	internal interface ILibraryApplicationWaitUntilInstallFinishedByIdStrategy
	{
		/// <summary>
		/// Waits until aplication will be installed.
		/// </summary>
		/// <param name="id">Application ID to check.</param>
		void WaitUntilInstallFinished(int id);
	}
}
