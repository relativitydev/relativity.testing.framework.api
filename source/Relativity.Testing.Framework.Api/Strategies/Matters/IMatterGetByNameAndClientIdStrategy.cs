using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the matter by name and client ID.
	/// </summary>
	internal interface IMatterGetByNameAndClientIdStrategy
	{
		/// <summary>
		/// Gets the entity by the specified name and client ID.
		/// </summary>
		/// <param name="name">The name.</param>
		/// <param name="clientId">The client ID.</param>
		/// <returns>The [Matter](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.Matter.html) object or <see langword="null"/>.</returns>
		Matter Get(string name, int clientId);
	}
}
