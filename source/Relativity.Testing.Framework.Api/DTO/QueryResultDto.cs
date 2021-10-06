using System.Collections.Generic;

namespace Relativity.Testing.Framework.Api.DTO
{
	/// <summary>
	/// Represents the results of the Query.
	/// </summary>
	/// <typeparam name="T">The type of the entities queried.</typeparam>
	public class QueryResultDto<T>
	{
		/// <summary>
		///  Gets or sets the value which indicates wheter more results are available than initially specified in the length property,
		///  or that result count exceeds the default query limit.
		///  You can retrieve the additional folders by using the QuerySubset.
		/// </summary>
		public string QueryToken { get; set; }

		/// <summary>
		/// Gets or sets the total numer of Artifacts returned by the Query.
		/// </summary>
		public int TotalCount { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether the overall set of read operations was a success.
		/// </summary>
		public bool Success { get; set; }

		/// <summary>
		/// Gets or sets message which contains the error information if the read operation failed.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the list of individual read results from the called operation.
		/// </summary>
		public List<QuerySingleResultDto<T>> Results { get; set; }
	}
}
