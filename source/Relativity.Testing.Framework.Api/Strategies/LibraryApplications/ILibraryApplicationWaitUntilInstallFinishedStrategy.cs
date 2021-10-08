namespace Relativity.Testing.Framework.Api.Strategies
{
	internal interface ILibraryApplicationWaitUntilInstallFinishedStrategy
	{
		void WaitUntilInstallFinished(int applicationId, int applicationInstallId);
	}
}
