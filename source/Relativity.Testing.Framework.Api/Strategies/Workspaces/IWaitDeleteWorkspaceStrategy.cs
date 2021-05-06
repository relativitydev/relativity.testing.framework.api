namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents wait for delete workspace strategy.
	/// </summary>
	internal interface IWaitDeleteWorkspaceStrategy
	{
		/// <summary>
		/// Waits the workdpace by the specified ID to be deleted.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		void Wait(int workspaceId);
	}
}
