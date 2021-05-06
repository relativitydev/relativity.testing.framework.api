namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	/// <summary>
	/// Represents the object of query slim result.
	/// </summary>
	public class QuerySlimObject
	{
		/// <summary>
		/// Gets or sets the artifact ID.
		/// </summary>
		public int ArtifactID { get; set; }

		/// <summary>
		/// Gets or sets the object field values.
		/// </summary>
		public object[] Values { get; set; }
	}
}
