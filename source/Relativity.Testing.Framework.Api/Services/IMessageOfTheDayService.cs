using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the message of the day (MotD) through the Notifications service.
	/// </summary>
	public interface IMessageOfTheDayService
	{
		/// <summary>
		/// Gets the message of the day.
		/// </summary>
		/// <returns>The <see cref="MessageOfTheDay"/> entity.</returns>
		MessageOfTheDay Get();

		/// <summary>
		/// Updates the message of the day.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		void Update(MessageOfTheDay entity);

		/// <summary>
		/// Dismiss message of the day for specified user
		/// by default dissmissing MotD for admin user.
		/// </summary>
		/// <param name="userId">The user artifact ID.</param>
		void Dismiss(int? userId = null);

		/// <summary>
		/// Determines whether user dismissed MotD.
		/// </summary>
		/// <param name="userId">The user artifact ID.</param>
		/// <returns><see langword="true"/> if an user dismissed MotD; otherwise, <see langword="false"/>.</returns>
		bool HasDismissed(int? userId = null);
	}
}
