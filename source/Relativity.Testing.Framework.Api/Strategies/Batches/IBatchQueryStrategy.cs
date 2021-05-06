using System;
using System.Linq.Expressions;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of batch query.
	/// </summary>
	internal interface IBatchQueryStrategy
	{
		/// <summary>
		/// Query the batches by specified condition.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="wherePropertySelector">The filter property selector.</param>
		/// <param name="whereValue">The filter property value.</param>
		/// <returns>The array of batches.</returns>
		Batch[] Query(int workspaceId, Expression<Func<Batch, object>> wherePropertySelector, object whereValue);
	}
}
