using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class MotdUpdateStrategyV1 : IMotdUpdateStrategy
	{
		private const string _PUT_URL = "relativity-infrastructure/v1/workspaces/-1/notifications";
		private readonly IRestService _restService;

		public MotdUpdateStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public MessageOfTheDay Update(MessageOfTheDay entity)
		{
			var dto = BuildDto(entity);

			return _restService.Put<MessageOfTheDay>(_PUT_URL, dto);
		}

		private object BuildDto(MessageOfTheDay entity)
		{
			return new
			{
				request = entity
			};
		}
	}
}
