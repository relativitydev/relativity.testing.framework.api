using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class MotdGetStrategyPreOsier : IMotdGetStrategy
	{
		private const string _POST_URL = "Relativity.Services.Notifications.INotificationsModule/Notifications/ReadMOTDAsync";
		private readonly IRestService _restService;

		public MotdGetStrategyPreOsier(IRestService restService)
		{
			_restService = restService;
		}

		public MessageOfTheDay Get()
		{
			return _restService.Post<MessageOfTheDay>(_POST_URL);
		}

		public async Task<MessageOfTheDay> GetAsync()
		{
			return await _restService.PostAsync<MessageOfTheDay>(_POST_URL).ConfigureAwait(false);
		}
	}
}
