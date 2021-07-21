using System.Threading.Tasks;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	/// <summary>
	/// Represents the message of the day (MotD) through the Notifications service.
	/// </summary>
	/// <example>
	/// <code>
	/// _messageOfTheDayService = relativityFacade.Resolve&lt;IMessageOfTheDayService&gt;();
	/// </code>
	/// </example>
	public interface IMessageOfTheDayService
	{
		/// <summary>
		/// Gets the message of the day.
		/// </summary>
		/// <returns>Returns an <see cref="MessageOfTheDay"/> instance.</returns>
		/// <example>
		/// <code>
		/// var currentMotd = _messageOfTheDayService.Get();
		/// </code>
		/// </example>
		MessageOfTheDay Get();

		/// <summary>
		/// Gets the message of the day.
		/// </summary>
		/// <returns>A <see cref="Task"/> with an <see cref="MessageOfTheDay"/> instance.</returns>
		/// <example>
		/// <code>
		/// var currentMotd = await _messageOfTheDayService.GetAsync().ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<MessageOfTheDay> GetAsync();

		/// <summary>
		/// Updates the message of the day.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <returns>Returns an <see cref="MessageOfTheDay"/> instance.</returns>
		/// <example>
		/// <code>
		/// var currentMotd = _messageOfTheDayService.Get();
		/// var toUpdate = new MessageOfTheDay
		/// {
		/// 	Enabled = !currentMotd.Enabled,
		/// 	Message = Randomizer.GetString(),
		/// 	AllowDismiss = !currentMotd.AllowDismiss
		/// };
		/// var updatedMotd = _messageOfTheDayService.Update(toUpdate);
		/// </code>
		/// </example>
		MessageOfTheDay Update(MessageOfTheDay entity);

		/// <summary>
		/// Updates the message of the day.
		/// </summary>
		/// <param name="entity">The entity to update.</param>
		/// <returns>Returns an <see cref="MessageOfTheDay"/> instance.</returns>
		/// <example>
		/// <code>
		/// var currentMotd = await _messageOfTheDayService.GetAsync().ConfigureAwait(false);
		/// var toUpdate = new MessageOfTheDay
		/// {
		/// 	Enabled = !currentMotd.Enabled,
		/// 	Message = Randomizer.GetString(),
		/// 	AllowDismiss = !currentMotd.AllowDismiss
		/// };
		/// var updatedMotd = await _messageOfTheDayService.UpdateAsync(toUpdate).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<MessageOfTheDay> UpdateAsync(MessageOfTheDay entity);

		/// <summary>
		/// Dismiss message of the day for specified user
		/// by default dissmissing MotD for admin user.
		/// </summary>
		/// <param name="userId">The user artifact ID.</param>
		/// <example>
		/// <code>
		/// var userArtifactID = 1015427;
		/// _messageOfTheDayService.Dismiss(userArtifactID);
		/// </code>
		/// </example>
		void Dismiss(int? userId = null);

		/// <summary>
		/// Dismiss message of the day for specified user
		/// by default dissmissing MotD for admin user.
		/// </summary>
		/// <param name="userId">The user artifact ID.</param>
		/// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
		/// <example>
		/// <code>
		/// var userArtifactID = 1015427;
		/// await _messageOfTheDayService.DismissAsync(userArtifactID).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task DismissAsync(int? userId = null);

		/// <summary>
		/// Determines whether user dismissed MotD.
		/// </summary>
		/// <param name="userId">The user artifact ID.</param>
		/// <returns><see langword="true"/> if an user dismissed MotD; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// var userArtifactID = 1015427;
		/// var hasDismissed = _messageOfTheDayService.HasDismissed(userArtifactID);
		/// </code>
		/// </example>
		bool HasDismissed(int? userId = null);

		/// <summary>
		/// Determines whether user dismissed MotD.
		/// </summary>
		/// <param name="userId">The user artifact ID.</param>
		/// <returns><see langword="true"/> if an user dismissed MotD; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// var userArtifactID = 1015427;
		/// var hasDismissed = await _messageOfTheDayService.HasDismissedAsync(userArtifactID).ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<bool> HasDismissedAsync(int? userId = null);

		/// <summary>
		/// Determines whether MotD is text-only.
		/// </summary>
		/// <returns><see langword="true"/> if MotD is text-only; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// var isTextOnly = _messageOfTheDayService.IsTextOnly();
		/// </code>
		/// </example>
		bool IsTextOnly();

		/// <summary>
		/// Determines whether MotD is text-only.
		/// </summary>
		/// <returns><see langword="true"/> if MotD is text-only; otherwise, <see langword="false"/>.</returns>
		/// <example>
		/// <code>
		/// var isTextOnly = await _messageOfTheDayService.IsTextOnlyAsync().ConfigureAwait(false);
		/// </code>
		/// </example>
		Task<bool> IsTextOnlyAsync();
	}
}
