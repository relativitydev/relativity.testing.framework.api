namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of check user dismissing.
	/// </summary>
	internal interface IMotdHasDismissedStrategy
	{
		/// <summary>
		/// Determines whether user dismissed MOTD.
		/// </summary>
		/// <param name="userId">The user ID.</param>
		/// <returns><see langword="true"/> if a user dismissed MOTD; otherwise, <see langword="false"/>.</returns>
		bool HasDismissed(int? userId = null);
	}
}
