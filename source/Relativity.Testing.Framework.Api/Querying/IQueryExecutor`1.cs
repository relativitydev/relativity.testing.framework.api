using System.Collections.Generic;

namespace Relativity.Testing.Framework.Api.Querying
{
	/// <summary>
	/// Represents an executor of object query.
	/// </summary>
	/// <typeparam name="TObject">The type of the object.</typeparam>
	public interface IQueryExecutor<TObject>
	{
		/// <summary>
		/// Executes the specified request and returns the query result objects.
		/// </summary>
		/// <param name="request">The request.</param>
		/// <returns>The collection of <typeparamref name="TObject"/> objects.</returns>
		IEnumerable<TObject> Execute(IQueryRequest request);
	}
}
