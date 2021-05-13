using kCura.Relativity.ImportAPI;
using Relativity.Testing.Framework.Configuration;

namespace Relativity.Testing.Framework.Api.Services
{
	internal class ImportApiService : ImportAPI, IImportApiService
	{
		public ImportApiService(IConfigurationService configurationService)
			: base
			(
				configurationService.RelativityInstance.AdminUsername,
				configurationService.RelativityInstance.AdminPassword,
				$"{configurationService.RelativityInstance.ServerBindingType}://{configurationService.RelativityInstance.RestServicesHostAddress}/RelativityWebApi")
		{
		}
	}
}
