using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class LibraryApplicationGetByIdStrategyV1 : IGetByIdStrategy<LibraryApplication>
	{
		private readonly IRestService _restService;

		public LibraryApplicationGetByIdStrategyV1(
			IRestService restService)
		{
			_restService = restService;
		}

		public LibraryApplication Get(int id)
		{
			return _restService.Get<LibraryApplication>($"relativity-environment/v1/workspace/-1/libraryapplications/{id}");
		}
	}
}
