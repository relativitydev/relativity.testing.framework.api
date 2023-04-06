namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the choice artifact ID of matter status by name.
	/// </summary>
	internal interface IMatterStatusGetChoiceIdByNameStrategy
	{
		/// <summary>
		/// Gets the artifact ID of matter status choice object by name.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <returns>The artifact ID.</returns>
		/// <exception>[ObjectNotFoundException](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.ObjectNotFoundException.html)The matter object is not found by name.</exception>
		int GetId(string name);
	}
}
