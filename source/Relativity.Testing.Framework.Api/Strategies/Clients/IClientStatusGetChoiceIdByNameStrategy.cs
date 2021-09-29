namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the choice artifact ID of client status by name.
	/// </summary>
	internal interface IClientStatusGetChoiceIdByNameStrategy
	{
		/// <summary>
		/// Gets the artifact ID of client status choice object by name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The artifact ID.</returns>
		/// [ObjectNotFoundException](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.ObjectNotFoundException.html)The choice object is not found by name.
		int GetId(string name);
	}
}
