using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class MotdGetStrategyV1 : IMotdGetStrategy
	{
		private const string _GET_URL = "relativity-infrastructure/v1/workspaces/-1/notifications";
		private readonly IRestService _restService;

		public MotdGetStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public MessageOfTheDay Get()
		{
			return _restService.Get<MessageOfTheDay>(_GET_URL);
		}

		public async Task<MessageOfTheDay> GetAsync()
		{
			return await _restService.GetAsync<MessageOfTheDay>(_GET_URL).ConfigureAwait(false);
		}
	}
}
