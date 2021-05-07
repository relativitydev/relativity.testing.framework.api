using System;
using Relativity.Services.ServiceProxy;
using Relativity.Testing.Framework.Configuration;

namespace Relativity.Testing.Framework.Api.Kepler
{
	internal class KeplerServiceFactory : IKeplerServiceFactory
	{
		private readonly Uri _restUri;

		private readonly Uri _rsapiUri;

		private readonly ServiceFactory _defaultFactory;

		public KeplerServiceFactory(IConfigurationService configurationService)
		{
			var relativityInstanceSettings = configurationService.RelativityInstance;

			_restUri = new Uri($"{relativityInstanceSettings.ServerBindingType}://{relativityInstanceSettings.RestServicesHostAddress}/relativity.rest/api");

			_rsapiUri = !string.IsNullOrEmpty(relativityInstanceSettings.RsapiServicesHostAddress)
				? new Uri($"{relativityInstanceSettings.ServerBindingType}://{relativityInstanceSettings.RsapiServicesHostAddress}/relativity.services")
				: _restUri;

			_defaultFactory = CreateServiceFactory(relativityInstanceSettings.AdminUsername, relativityInstanceSettings.AdminPassword);
		}

		public T GetServiceProxy<T>()
			where T : IDisposable
		{
			return _defaultFactory.CreateProxy<T>();
		}

		public T GetServiceProxy<T>(string username, string password)
			where T : IDisposable
		{
			ServiceFactory serviceFactory = CreateServiceFactory(username, password);
			return serviceFactory.CreateProxy<T>();
		}

		private ServiceFactory CreateServiceFactory(string username, string password)
		{
			UsernamePasswordCredentials credentials = new UsernamePasswordCredentials(username, password);
			ServiceFactorySettings settings = new ServiceFactorySettings(_rsapiUri, _restUri, credentials);

			return new ServiceFactory(settings);
		}
	}
}
