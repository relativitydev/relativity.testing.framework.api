namespace Relativity.Testing.Framework.Api.Strategies
{
	/// <summary>
	/// Represents the strategy of dismissing message of the day.
	/// </summary>
	internal interface IMotdDismissStrategy
	{
		/// <summary>
		/// Dismiss message of the day for specified user
		/// by default dissmissing MOTD for admin user.
		/// </summary>
		/// <param name="userId">The user ID.</param>
		void Dismiss(int? userId = null);
	}
}
