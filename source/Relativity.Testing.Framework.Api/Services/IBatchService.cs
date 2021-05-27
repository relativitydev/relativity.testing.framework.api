using System;
using System.Linq.Expressions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the batch API service.
	/// </summary>
	public interface IBatchService
	{
		/// <summary>
		/// Gets the batch by the specified ID.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get the batch.</param>
		/// <param name="entityId">The Artifact ID of the batch.</param>
		/// <returns>>The <see cref="Batch"/> entity or <see langword="null"/>.</returns>
		Batch Get(int workspaceId, int entityId);

		/// <summary>
		/// Gets all batches.
		/// </summary>
		/// <param name="workspaceId">The Artifact ID of the workspace where you want to get all batches.</param>
		/// <returns>The collection of <see cref="Batch"/> entities.</returns>
		/// <example>
		/// <code>
		/// RelativityFacade.Instance.RelyOn&lt;CoreComponent&gt;();
		/// RelativityFacade.Instance.RelyOn&lt;ApiComponent&gt;();
		/// IBatchService _batchService = RelativityFacade.Instance.Resolve&lt;IBatchService&gt;();
		/// List&lt;Batch&gt; batches = _batchService.GetAll(1015427);
		/// </code>
		/// </example>
		Batch[] GetAll(int workspaceId);

		/// <summary>
		/// Query the batches by specified condition.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="wherePropertySelector">The filter property selector.</param>
		/// <param name="whereValue">The filter property value.</param>
		/// <returns>The array of batches.</returns>
		Batch[] Query(int workspaceId, Expression<Func<Batch, object>> wherePropertySelector, object whereValue);

		/// <summary>
		/// Assign a batch to a user.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="batchId">The batch ID.</param>
		/// <param name="userId">The user ID.</param>
		void AssignToUser(int workspaceId, int batchId, int userId);
	}
}
