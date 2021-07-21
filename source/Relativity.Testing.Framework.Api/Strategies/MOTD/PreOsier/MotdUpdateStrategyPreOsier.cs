using System.Threading.Tasks;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class MotdUpdateStrategyPreOsier : IMotdUpdateStrategy
	{
		private const string _POST_URL = "Relativity.Services.Notifications.INotificationsModule/Notifications/UpdateMOTDAsync";
		private readonly IRestService _restService;
		private readonly IMotdGetStrategy _motdGetStrategy;

		public MotdUpdateStrategyPreOsier(IRestService restService, IMotdGetStrategy motdGetStrategy)
		{
			_restService = restService;
			_motdGetStrategy = motdGetStrategy;
		}

		public MessageOfTheDay Update(MessageOfTheDay entity)
		{
			var dto = BuildDto(entity);

			_restService.Post(_POST_URL, dto);

			return _motdGetStrategy.Get();
		}

		public async Task<MessageOfTheDay> UpdateAsync(MessageOfTheDay entity)
		{
			var dto = BuildDto(entity);

			await _restService.PostAsync(_POST_URL, dto).ConfigureAwait(false);

			return await _motdGetStrategy.GetAsync().ConfigureAwait(false);
		}

		private object BuildDto(MessageOfTheDay entity)
		{
			return new
			{
				motd = entity
			};
		}
	}
}
