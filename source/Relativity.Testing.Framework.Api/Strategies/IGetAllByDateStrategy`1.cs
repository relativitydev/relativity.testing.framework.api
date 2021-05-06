using System;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting all entities of particular type by specific date.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	internal interface IGetAllByDateStrategy<T>
	{
		/// <summary>
		/// Gets all entities by specific date.
		/// </summary>
		/// <param name="from">The start of a date range.</param>
		/// <param name="to">The end of a date range.</param>
		/// <returns>The entities.</returns>
		T[] GetAll(DateTime from, DateTime to);
	}
}
