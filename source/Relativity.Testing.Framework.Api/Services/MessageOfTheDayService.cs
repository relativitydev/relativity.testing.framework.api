using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class MessageOfTheDayService : IMessageOfTheDayService
	{
		private readonly IMotdGetStrategy _motdGetStrategy;
		private readonly IMotdUpdateStrategy _updateStrategy;
		private readonly IMotdDismissStrategy _dismissMotdStrategy;
		private readonly IMotdHasDismissedStrategy _hasDismissMotdStrategy;
		private readonly IMotdIsTextOnlyStrategy _motdIsTextOnlyStrategy;

		public MessageOfTheDayService(
			IMotdGetStrategy motdGetStrategy,
			IMotdUpdateStrategy updateStrategy,
			IMotdDismissStrategy dismissMotdStrategy,
			IMotdHasDismissedStrategy hasDismissMotdStrategy,
			IMotdIsTextOnlyStrategy motdIsTextOnlyStrategy)
		{
			_motdGetStrategy = motdGetStrategy;
			_updateStrategy = updateStrategy;
			_dismissMotdStrategy = dismissMotdStrategy;
			_hasDismissMotdStrategy = hasDismissMotdStrategy;
			_motdIsTextOnlyStrategy = motdIsTextOnlyStrategy;
		}

		public MessageOfTheDay Get()
			=> _motdGetStrategy.Get();

		public async Task<MessageOfTheDay> GetAsync()
			=> await _motdGetStrategy.GetAsync().ConfigureAwait(false);

		public MessageOfTheDay Update(MessageOfTheDay entity)
			=> _updateStrategy.Update(entity);

		public async Task<MessageOfTheDay> UpdateAsync(MessageOfTheDay entity)
			=> await _updateStrategy.UpdateAsync(entity).ConfigureAwait(false);

		public void Dismiss(int? userId = null)
			=> _dismissMotdStrategy.Dismiss(userId);

		public void Dismiss(string emailAddress)
			=> _dismissMotdStrategy.Dismiss(emailAddress);

		public async Task DismissAsync(int? userId = null)
			=> await _dismissMotdStrategy.DismissAsync(userId).ConfigureAwait(false);

		public bool HasDismissed(int? userId = null)
			=> _hasDismissMotdStrategy.HasDismissed(userId);

		public async Task<bool> HasDismissedAsync(int? userId = null)
			=> await _hasDismissMotdStrategy.HasDismissedAsync(userId).ConfigureAwait(false);

		public bool IsTextOnly()
			=> _motdIsTextOnlyStrategy.IsTextOnly();

		public async Task<bool> IsTextOnlyAsync()
			=> await _motdIsTextOnlyStrategy.IsTextOnlyAsync().ConfigureAwait(false);
	}
}
