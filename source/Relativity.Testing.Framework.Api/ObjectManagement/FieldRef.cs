using System;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	/// <summary>
	/// Represents a key or reference to a Field object.
	/// </summary>
	public class FieldRef
	{
		/// <summary>
		/// Gets or sets a unique identifier used to reference a view field.
		/// </summary>
		public int ViewFieldID { get; set; }

		/// <summary>
		/// Gets or sets the Artifact ID of a Field object.
		/// </summary>
		public int ArtifactID { get; set; }

		/// <summary>
		/// Gets or sets a GUID used to identify a Field object.
		/// </summary>
		public Guid? Guid { get; set; }

		/// <summary>
		/// Gets or sets the user-friendly name of a Field object.
		/// </summary>
		public string Name { get; set; }
	}
}
