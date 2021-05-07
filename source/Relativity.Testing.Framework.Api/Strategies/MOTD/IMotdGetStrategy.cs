using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting message of the day.
	/// </summary>
	internal interface IMotdGetStrategy
	{
		/// <summary>
		/// Gets message of the day object.
		/// </summary>
		/// <returns>The message of the day object.</returns>
		MessageOfTheDay Get();
	}
}
