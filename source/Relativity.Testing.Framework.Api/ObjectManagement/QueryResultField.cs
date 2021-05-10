using System;
using System.Collections.Generic;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	/// <summary>
	/// Represents the field of object query result.
	/// </summary>
	public class QueryResultField
	{
		/// <summary>
		/// Gets or sets the artifact ID.
		/// </summary>
		public int? ArtifactID { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// Gets or sets the type of the field.
		/// </summary>
		public string FieldType { get; set; }

		/// <summary>
		/// Gets or sets the field category.
		/// </summary>
		public string FieldCategory { get; set; }

		/// <summary>
		/// Gets or sets the list of guids associated with the field.
		/// </summary>
		public List<Guid> Guids { get; set; }
	}
}
