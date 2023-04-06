using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Strategies;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class LibraryApplicationDeleteByIdStrategyV1 : IDeleteByIdStrategy<LibraryApplication>
	{
		private readonly IRestService _restService;

		public LibraryApplicationDeleteByIdStrategyV1(IRestService restService)
		{
			_restService = restService;
		}

		public void Delete(int id)
		{
			_restService.Delete($"relativity-environment/v1/workspace/-1/libraryapplications/{id}");
		}
	}
}
