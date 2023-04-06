using System.Net.Http;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange(">=12.1")]
	internal class LibraryApplicationGetByIdStrategyV1 : IGetByIdStrategy<LibraryApplication>
	{
		private const string _ENTITY_NOT_FOUND_EXCEPTION_MESSAGE = "The object does not exist or you do not have permission to access it.";
		private readonly IRestService _restService;

		public LibraryApplicationGetByIdStrategyV1(
			IRestService restService)
		{
			_restService = restService;
		}

		public LibraryApplication Get(int id)
		{
			try
			{
				return _restService.Get<LibraryApplication>($"relativity-environment/v1/workspace/-1/libraryapplications/{id}");
			}
			catch (HttpRequestException exception) when (exception.Message.Contains(_ENTITY_NOT_FOUND_EXCEPTION_MESSAGE))
			{
				return null;
			}
		}
	}
}
