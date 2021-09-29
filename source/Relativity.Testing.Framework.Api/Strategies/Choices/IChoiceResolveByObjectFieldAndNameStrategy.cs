using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of resolving/getting the choice artifact reference for particular object field by choice name.
	/// </summary>
	internal interface IChoiceResolveByObjectFieldAndNameStrategy
	{
		/// <summary>
		/// Gets the choice artifact reference for particular object field within workspace by choice name.
		/// Throws [ObjectNotFoundException](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.ObjectNotFoundException.html) if choice is not found.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="objectTypeName">Name of the object type.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="choiceName">Name of the choice.</param>
		/// <returns>The [ArtifactReference](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ArtifactReference.html) object.</returns>
		/// [ObjectNotFoundException](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.ObjectNotFoundException.html)
		/// The choice object is not found.
		/// </exception>
		ArtifactReference ResolveReference(int workspaceId, string objectTypeName, string fieldName, string choiceName);

		/// <summary>
		/// Gets the choice artifact reference for particular object field by choice name.
		/// Throws [ObjectNotFoundException](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.ObjectNotFoundException.html) if choice is not found.
		/// </summary>
		/// <param name="objectTypeName">Name of the object type.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <param name="choiceName">Name of the choice.</param>
		/// <returns>The [ArtifactReference](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.ArtifactReference.html) object.</returns>
		/// [ObjectNotFoundException](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.ObjectNotFoundException.html)
		/// The choice object is not found.
		/// </exception>
		ArtifactReference ResolveReference(string objectTypeName, string fieldName, string choiceName);
	}
}
