using System;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the entity by GUID.
	/// </summary>
	/// <typeparam name="T">The type of the entity.</typeparam>
	public interface IGetByGuidStrategy<T>
	{
		/// <summary>
		/// Gets the entity by the specified GUID.
		/// </summary>
		/// <param name="identifier">The entity GUID.</param>
		/// <returns>The entity or <see langword="null"/>.</returns>
		T Get(Guid identifier);
	}
}
