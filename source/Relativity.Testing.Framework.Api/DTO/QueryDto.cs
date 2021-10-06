using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.DTO
{
	/// <summary>
	/// Represents the Query DTO.
	/// </summary>
	public class QueryDTO
	{
		/// <summary>
		/// Gets or sets condition for the query.
		/// </summary>
		public string Condition { get; set; }

		/// <summary>
		/// Gets or sets list of sorts for the query.
		/// </summary>
		public List<Sort> Sorts { get; set; }
	}
}
