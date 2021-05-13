using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the user by email address.
	/// </summary>
	internal interface IUserGetByEmailStrategy
	{
		/// <summary>
		/// Gets the user by the specified email address.
		/// </summary>
		/// <param name="email">The email address.</param>
		/// <returns>The <see cref="User"/> entity or <see langword="null"/>.</returns>
		User Get(string email);
	}
}
