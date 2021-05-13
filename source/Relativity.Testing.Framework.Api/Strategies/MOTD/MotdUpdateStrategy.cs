using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Strategies
{
	internal class MotdUpdateStrategy : IUpdateStrategy<MessageOfTheDay>
	{
		private readonly IRestService _restService;

		public MotdUpdateStrategy(IRestService restService)
		{
			_restService = restService;
		}

		public void Update(MessageOfTheDay entity)
		{
			var dto = new
			{
				motd = entity
			};

			_restService.Post("Relativity.Services.Notifications.INotificationsModule/Notifications/UpdateMOTDAsync", dto);
		}
	}
}
