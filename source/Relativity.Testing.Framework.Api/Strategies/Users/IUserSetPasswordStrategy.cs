using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy to set a user's password.
	/// </summary>
	internal interface IUserSetPasswordStrategy
	{
		/// <summary>
		/// Sets the password for a user to a specified value.
		/// </summary>
		/// <param name="userArtifactID">The ArtifactID of the user.</param>
		/// <param name="password">The new password for the user.</param>
		void SetPassword(int userArtifactID, string password);
	}
}
