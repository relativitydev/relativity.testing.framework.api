using System.Collections.Generic;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting all entities of particular type by specified names.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IGetAllByNamesStrategy<T>
	{
		/// <summary>
		/// Gets all entities by specified names.
		/// </summary>
		/// <param name="names">The list of group names.</param>
		/// <returns>The entities.</returns>
		IEnumerable<T> GetAll(IEnumerable<string> names);
	}
}
