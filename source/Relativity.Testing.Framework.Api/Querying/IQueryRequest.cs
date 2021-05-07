using System.Collections.Generic;

namespace Relativity.Testing.Framework.Api.Querying
{
	/// <summary>
	/// Represents the generic query request.
	/// </summary>
	public interface IQueryRequest
	{
		/// <summary>
		/// Adds the condition.
		/// </summary>
		/// <param name="rowCondition">The row condition.</param>
		void AddCondition(string rowCondition);

		/// <summary>
		/// Adds the condition.
		/// </summary>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="fieldValue">The field value.</param>
		void AddCondition(string fieldName, object fieldValue);

		/// <summary>
		/// Sets the fields to fetch.
		/// </summary>
		/// <param name="fieldNames">The field names.</param>
		void SetFieldsToFetch(IEnumerable<string> fieldNames);

		/// <summary>
		/// Specifies the  number of items to return in the query result.
		/// </summary>
		/// <param name="length">The number of items to return in the query result, starting with index in the start parameter.</param>
		void SetLength(int length);
	}
}
