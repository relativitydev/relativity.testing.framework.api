namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of getting the existance of the user by email address.
	/// </summary>
	internal interface IUserExistsByEmailStrategy
	{
		/// <summary>
		/// Determines whether the user with the specified email address exists.
		/// </summary>
		/// <param name="email">The email address.</param>
		/// <returns><see langword="true"/> if a user exists; otherwise, <see langword="false"/>.</returns>
		bool Exists(string email);
	}
}
