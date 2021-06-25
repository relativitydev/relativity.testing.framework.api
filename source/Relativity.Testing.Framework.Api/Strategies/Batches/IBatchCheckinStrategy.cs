namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of batch checkin.
	/// </summary>
	internal interface IBatchCheckinStrategy
	{
		/// <summary>
		/// Checks in the batch.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="batchId">The Artifact ID of the batch.</param>
		/// <param name="isCompleted">Indicator if Batch is completed or not.
		/// <para>When set to true - updates the batch status to Completed. The batch remains assigned to the current user.</para>
		/// <para>When set to false - updates the batch status to empty string and removes the user assignment. </para>
		/// </param>
		void Checkin(int workspaceId, int batchId, bool isCompleted);
	}
}
