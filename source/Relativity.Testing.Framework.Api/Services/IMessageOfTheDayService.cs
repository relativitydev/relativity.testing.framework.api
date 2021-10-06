using Relativity.Testing.Framework.Configuration;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the message of the day (MotD) through the Notifications service.
	/// </summary>
	/// <example>
	/// <code>
	/// IMessageOfTheDayService _messageOfTheDayService = relativityFacade.Resolve&lt;IMessageOfTheDayService&gt;();
	/// </code>
	/// </example>
	public interface IMessageOfTheDayService
	{
		/// <summary>
		/// Gets the message of the day.
		/// </summary>
		/// <returns>Returns an [MessageOfTheDay](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MessageOfTheDay.html) instance.</returns>
		/// <example>
		/// <code>
		/// MessageOfTheDay currentMotd = _messageOfTheDayService.Get();
		/// </code>
		/// </example>
		MessageOfTheDay Get();

		/// <summary>
		/// Updates the message of the day.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <returns>Returns an [MessageOfTheDay](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Models.MessageOfTheDay.html) instance.</returns>
		/// <example>
		/// <code>
		/// MessageOfTheDay currentMotd = _messageOfTheDayService.Get();
		/// var toUpdate = new MessageOfTheDay
		/// {
		///     Enabled = !currentMotd.Enabled,
		///     Message = Randomizer.GetString(),
		///     AllowDismiss = !currentMotd.AllowDismiss
		/// };
		/// MessageOfTheDay updatedMotd = _messageOfTheDayService.Update(toUpdate);
		/// </code>
		/// </example>
		MessageOfTheDay Update(MessageOfTheDay entity);

		/// <summary>
		/// Dismiss message of the day for the user with the specified ArtifactID
		/// by default dismissing MotD for Admin User provided by [AdminUsername](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Configuration.RelativityInstanceConfiguration.html#Relativity_Testing_Framework_Configuration_RelativityInstanceConfiguration_AdminUsername).
		/// </summary>
		/// <param name="userId">The user artifact ID.</param>
		/// <example>
		/// <code>
		/// int userArtifactID = 1015427;
		/// _messageOfTheDayService.Dismiss(userArtifactID);
		/// </code>
		/// </example>
		void Dismiss(int? userId = null);

		/// <summary>
		/// Dismiss message of the day for the user with the specified email address.
		/// </summary>
		/// <param name="emailAddress">The user email address.</param>
		/// <example>
		/// <code>
		/// string userEmailAddress = AUser@test.com;
		/// _messageOfTheDayService.Dismiss(userEmailAddress);
		/// </code>
		/// </example>
		void Dismiss(string emailAddress);

		/// <summary>
		/// Determines whether specific user dismissed MotD.
		/// by default determining MotD dismissal for Admin User provided by [AdminUsername](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.Configuration.RelativityInstanceConfiguration.html#Relativity_Testing_Framework_Configuration_RelativityInstanceConfiguration_AdminUsername).
		/// </summary>
		/// <param name="userId">The user artifact ID.</param>
		/// <returns><see langword="true"/> if an user dismissed MotD; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// int userArtifactID = 1015427;
		/// bool hasDismissed = _messageOfTheDayService.HasDismissed(userArtifactID);
		/// </code>
		/// </example>
		bool HasDismissed(int? userId = null);

		/// <summary>
		/// Determines whether MotD is text-only.
		/// </summary>
		/// <returns><see langword="true"/> if MotD is text-only; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// bool isTextOnly = _messageOfTheDayService.IsTextOnly();
		/// </code>
		/// </example>
		bool IsTextOnly();
	}
}
