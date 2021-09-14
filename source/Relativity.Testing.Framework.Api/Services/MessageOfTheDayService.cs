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

		public MessageOfTheDay Update(MessageOfTheDay entity)
			=> _updateStrategy.Update(entity);

		public void Dismiss(int? userId = null)
			=> _dismissMotdStrategy.Dismiss(userId);

		public void Dismiss(string emailAddress)
			=> _dismissMotdStrategy.Dismiss(emailAddress);

		public bool HasDismissed(int? userId = null)
			=> _hasDismissMotdStrategy.HasDismissed(userId);

		public bool IsTextOnly()
			=> _motdIsTextOnlyStrategy.IsTextOnly();
	}
}
