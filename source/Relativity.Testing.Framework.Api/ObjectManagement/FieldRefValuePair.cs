namespace Relativity.Testing.Framework.Api.ObjectManagement
{
	/// <summary>
	/// Represents a key or reference to a Field object and the value currently assigned to it.
	/// </summary>
	/// <remarks>
	/// <list type="table">
	/// <listheader>
	/// <term>FieldType</term>
	/// <term>Expected value type</term>
	/// </listheader>
	/// <item>
	/// <term >Fixed-Length Text</term>
	/// <term>string</term>
	/// </item>
	/// <item>
	/// <term>Long Text</term>
	/// <term>string</term>
	/// </item>
	/// <item>
	/// <term >Date</term>
	/// <term >DateTime</term>
	/// </item>
	/// <item>
	/// <term>Whole Number</term>
	/// <term >int?</term>
	/// </item>
	/// <item>
	/// <term >Decimal</term>
	/// <term >decimal?</term>
	/// </item>
	/// <item>
	/// <term>Currency</term>
	/// <term >decimal?</term>
	/// </item>
	/// <item>
	/// <term >Yes/No</term>
	/// <term >bool?</term>
	/// </item>
	/// <item>
	/// <term>Single Choice</term>
	/// <term >ChoiceRef</term>
	/// </item>
	/// <item>
	/// <term >Multiple Choice</term>
	/// <term >IEnumerable&lt; ChoiceRef&gt;</term>
	/// </item>
	/// <item>
	/// <term>User</term>
	/// <term >User(The ArtifactID must be set on this object.)</term>
	/// </item>
	/// <item>
	/// <term >File</term>
	/// <term >FileRef</term>
	/// </item>
	/// <item>
	/// <term>Single Object</term>
	/// <term >RelativityObjectRef</term>
	/// </item>
	/// <item>
	/// <term >Multiple Object</term>
	/// <term >IEnumerable&lt; RelativityObjectRef&gt;</term>
	/// </item>
	/// </list>
	/// </remarks>
	public class FieldRefValuePair
	{
		/// <summary>
		/// Gets or sets the value assigned to a field.
		/// </summary>
		public object Value { get; set; }

		/// <summary>
		/// Gets or sets a <see cref="FieldRef"/> object.
		/// </summary>
		public FieldRef Field { get; set; }
	}
}
