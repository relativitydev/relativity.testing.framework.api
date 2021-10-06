using System.Collections.Generic;

namespace Relativity.Testing.Framework.Api.DTO
{
	/// <summary>
	/// Represents the single result of the query.
	/// </summary>
	/// <typeparam name="T">The type of entity result.</typeparam>
	public class QuerySingleResultDto<T>
	{
		/// <summary>
		/// Gets or sets a value indicating whether the operation was a success.
		/// </summary>
		public bool Success { get; set; }

		/// <summary>
		/// Gets or sets the artifact result.
		/// </summary>
		public T Artifact { get; set; }

		/// <summary>
		/// Gets or sets the message which provides information about the failure.
		/// </summary>
		public string Message { get; set; }

		/// <summary>
		/// Gets or sets the message which provides information about the warnings.
		/// </summary>
		public List<string> WarningMessages { get; set; }
	}
}
