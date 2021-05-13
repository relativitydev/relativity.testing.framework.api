using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class DismissMotdStrategy : IMotdGetStrategy
	{
		private readonly IRestService _restService;

		public DismissMotdStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public MessageOfTheDay Get()
		{
			return _restService.Post<MessageOfTheDay>("Relativity.Services.Notifications.INotificationsModule/Notifications/ReadMOTDAsync");
		}
	}
}
