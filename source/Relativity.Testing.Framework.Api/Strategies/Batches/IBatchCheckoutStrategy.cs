namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of batch checkout.
	/// </summary>
	internal interface IBatchCheckoutStrategy
	{
		/// <summary>
		/// Checks out the batch.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="batchId">The Artifact ID of the batch.</param>
		/// <param name="userId">The Artifact ID for the user who should be assigned to the batch.</param>
		void Checkout(int workspaceId, int batchId, int userId);
	}
}
