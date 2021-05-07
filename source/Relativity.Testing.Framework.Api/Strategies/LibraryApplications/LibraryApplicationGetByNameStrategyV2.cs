using System;
using System.Linq;
using Newtonsoft.Json.Linq;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Mapping;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Strategies
{
	[VersionRange("<12.1")]
	internal class LibraryApplicationGetByNameStrategyV2 : IGetByNameStrategy<LibraryApplication>
	{
		private readonly IRestService _restService;

		private readonly IJsonObjectMappingService _jsonObjectMappingService;

		public LibraryApplicationGetByNameStrategyV2(
			IRestService restService,
			IJsonObjectMappingService jsonObjectMappingService)
		{
			_restService = restService;
			_jsonObjectMappingService = jsonObjectMappingService;
		}

		public LibraryApplication Get(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException(nameof(name));
			}

			var responseObjects = _restService.Get<JObject[]>("Relativity.LibraryApplications/workspace/-1/libraryapplications");

			var applications = _jsonObjectMappingService.MapTo<LibraryApplication>(responseObjects);

			return applications.FirstOrDefault(x => x.Name == name);
		}
	}
}
