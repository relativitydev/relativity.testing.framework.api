using System.Collections.Generic;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting all entities of particular type by specified client name.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IGetAllByClientNameStrategy<T>
	{
		/// <summary>
		/// Gets all entities by specified client name.
		/// </summary>
		/// <param name="clientName">The client name.</param>
		/// <returns>The entities.</returns>
		IEnumerable<T> GetAllByClientName(string clientName);
	}
}
