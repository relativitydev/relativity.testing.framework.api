using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class MessageOfTheDayService : IMessageOfTheDayService
	{
		private readonly IMotdGetStrategy _motdGetStrategy;
		private readonly IUpdateStrategy<MessageOfTheDay> _updateStrategy;
		private readonly IMotdDismissStrategy _dismissMotdStrategy;
		private readonly IMotdHasDismissedStrategy _hasDismissMotdStrategy;

		public MessageOfTheDayService(
			IMotdGetStrategy motdGetStrategy,
			IUpdateStrategy<MessageOfTheDay> updateStrategy,
			IMotdDismissStrategy dismissMotdStrategy,
			IMotdHasDismissedStrategy hasDismissMotdStrategy)
		{
			_motdGetStrategy = motdGetStrategy;
			_updateStrategy = updateStrategy;
			_dismissMotdStrategy = dismissMotdStrategy;
			_hasDismissMotdStrategy = hasDismissMotdStrategy;
		}

		public MessageOfTheDay Get()
			=> _motdGetStrategy.Get();

		public void Update(MessageOfTheDay entity)
			=> _updateStrategy.Update(entity);

		public void Dismiss(int? userId = null)
			=> _dismissMotdStrategy.Dismiss(userId);

		public bool HasDismissed(int? userId = null)
			=> _hasDismissMotdStrategy.HasDismissed(userId);
	}
}
