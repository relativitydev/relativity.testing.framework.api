using System.Collections.Generic;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting all choices for particular object field.
	/// </summary>
	internal interface IChoiceGetAllByObjectFieldStrategy
	{
		/// <summary>
		/// Gets all choices for particular object field.
		/// </summary>
		/// <param name="workspaceId">The workspace ID.</param>
		/// <param name="objectTypeName">Name of the object type.</param>
		/// <param name="fieldName">Name of the field.</param>
		/// <returns>The collection of choices.</returns>
		IEnumerable<Choice> GetAll(int workspaceId, string objectTypeName, string fieldName);
	}
}
