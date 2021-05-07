using System;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	/// <summary>
	/// Represents the field of object query request.
	/// </summary>
	public class QueryField
	{
		/// <summary>
		/// Gets or sets the artifact ID.
		/// </summary>
		public int? ArtifactID { get; set; }

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
