using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class MotdIsTextOnlyStrategyPreOsier : IMotdIsTextOnlyStrategy
	{
		private const string _POST_URL = "Relativity.Services.Notifications.INotificationsModule/Notifications/IsTextOnlyMOTDAsync";
		private readonly IRestService _restService;

		public MotdIsTextOnlyStrategyPreOsier(IRestService restService)
		{
			_restService = restService;
		}

		public bool IsTextOnly()
		{
			return _restService.Post<bool>(_POST_URL);
		}
	}
}
