using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api.Versioning
{
	/// <summary>
	/// Represents the service that resolves the version of Relativity instance by getting the version thru REST API request.
	/// </summary>
	internal class ApiRelativityInstanceVersionResolveService : IRelativityInstanceVersionResolveService
	{
		private readonly IRestService _restService;

		public ApiRelativityInstanceVersionResolveService(IRestService restService)
		{
			_restService = restService;
		}

		public string GetVersion()
		{
			string versionString = _restService.Post<string>("Relativity.Services.InstanceDetails.IInstanceDetailsModule/InstanceDetailsService/GetRelativityVersionAsync");
			return versionString.Trim('"');
		}
	}
}
