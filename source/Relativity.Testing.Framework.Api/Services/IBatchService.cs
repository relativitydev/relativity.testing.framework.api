using System;
using System.Linq.Expressions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the batch API service.
	/// </summary>
	/// <example>
	/// <code>
	/// _batchService = relativityFacade.Resolve&lt;IBatchService&gt;();
	/// </code>
	/// </example>
	public interface IBatchService
	{
		/// <summary>
		/// Gets the batch by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the batch.</param>
		/// <param name="entityId">The Artifact ID of the batch.</param>
		/// <returns>The [Batch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Batch.html) entity or <see langword="null"/>.</returns>
		/// <example>
		/// <code>
		/// Batch batch = _batchService.Get(1015427, batchArtifactId);
		/// </code>
		/// </example>
		Batch Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets all batches.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get all batches.</param>
		/// <returns>The collection of [Batches](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Batch.html) entities.</returns>
		/// <example>
		/// <code>
		/// List&lt;Batch&gt; batches = _batchService.GetAll(1015427);
		/// </code>
		/// </example>
		Batch[] GetAll(int workspaceId);

		/// <summary>
		/// Query the [Batch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Batch.html) by specified condition.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="wherePropertySelector">The filter property selector.</param>
		/// <param name="whereValue">The filter property value.</param>
		/// <returns>The array of [Batches](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Batch.html).</returns>
		/// <example>
		/// <code>
		/// var matchingBatches = _batchService.Query(1015427, x => x.BatchSet, "Batch Set Name"); /// Finding batches from workspace with ID 1015427 that have BatchSet "Batch Set Name"
		/// </code>
		/// </example>
		Batch[] Query(int workspaceId, Expression<Func<Batch, object>> wherePropertySelector, object whereValue);

		/// <summary>
		/// Assign a [Batch](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Batch.html) to a user.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="batchId">The batch ID.</param>
		/// <param name="userId">The user ID.</param>
		/// <example>
		/// <code>
		/// var batch = _batchService.Get(1015427, batchArtifactId);
		/// user = Facade.Resolve&lt;IUserService&gt;().Get(someUserId);
		/// _batchService.AssignToUser(1015427, batch.ArtifactID, user.ArtifactID);
		/// </code>
		/// </example>
		void AssignToUser(int workspaceId, int batchId, int userId);

		/// <summary>
		/// Checks in the batch.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="batchId">The Artifact ID of the batch.</param>
		/// <param name="isCompleted">Indicator if Batch is completed or not.
		/// <para>When set to true - updates the batch status to Completed. The batch remains assigned to the current user.</para>
		/// <para>When set to false - updates the batch status to empty string and removes the user assignment. </para>
		/// </param>
		/// <example>
		/// <code>
		/// Facade.Resolve&lt;IBatchService&gt;().Checkin(workspaceArtifactId, batchArtifactID, true);
		/// </code>
		/// </example>
		void Checkin(int workspaceId, int batchId, bool isCompleted);

		/// <summary>
		/// Checks out the batch.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="batchId">The Artifact ID of the batch.</param>
		/// <param name="userId">The Artifact ID for the user who should be assigned to the batch.</param>
		/// <example>
		/// <code>
		/// Facade.Resolve&lt;IBatchService&gt;().Checkout(workspaceArtifactId, batchArtifactID, userArtifactId);
		/// </code>
		/// </example>
		void Checkout(int workspaceId, int batchId, int userId);
	}
}
