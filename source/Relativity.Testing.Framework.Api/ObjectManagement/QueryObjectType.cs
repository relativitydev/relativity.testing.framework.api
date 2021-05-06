using System;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	/// <summary>
	/// Represents the query object type.
	/// </summary>
	public class QueryObjectType
	{
		/// <summary>
		/// Gets or sets the artifact type ID.
		/// </summary>
		public int? ArtifactTypeID { get; set; }

		/// <summary>
		/// Gets or sets the Guid.
		/// </summary>
		public Guid? Guid { get; set; }

		/// <summary>
		/// Gets or sets the name.
		/// </summary>
		public string Name { get; set; }
	}
}
