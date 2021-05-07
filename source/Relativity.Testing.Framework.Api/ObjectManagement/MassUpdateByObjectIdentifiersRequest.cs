using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	/// <summary>
	/// Represents a request to perform a mass update operation on a list of Documents or Relativity Dynamic Objects (RDOs).
	/// by modifying each of the specified object fields with the same value.
	/// </summary>
	/// <remarks>
	/// The MassUpdateByObjectIdentifiersRequest class specifies the identifiers used
	/// to select a list of objects with the same type for updating. It also includes
	/// the same values for all object fields that are to be updated. In the Relativity UI,
	/// this update operation is equivalent to the user selecting the Checked or
	/// These option in the mass operations bar on a list page.
	/// </remarks>
	public class MassUpdateByObjectIdentifiersRequest
	{
		/// <summary>
		/// Gets or sets a list of objects to be updated.
		/// </summary>
		public IEnumerable<Artifact> Objects { get; set; }

		/// <summary>
		/// Gets or sets the fields and values used for updating each object. The same value is used for updating all object fields that are to be updated.
		/// </summary>
		public IEnumerable<FieldRefValuePair> FieldValues { get; set; }
	}
}
