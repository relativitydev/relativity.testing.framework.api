using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.Configuration;
using Relativity.Testing.Framework.Configuration;
using Relativity.Testing.Framework.Session;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	/// <summary>
	/// Represents the host of <see cref="IRelativityFacade"/> that provides methods for facade setup, configuration and cleanup.
	/// </summary>
	public class RelativityFacadeHost
	{
		public RelativityFacadeHost()
		{
			RelativityInstanceConfiguration = ApiTestFixture.Service.RelativityInstance;
		}

		/// <summary>
		/// Gets current Relativity instance configuration.
		/// </summary>
		public RelativityInstanceConfiguration RelativityInstanceConfiguration { get; }

		/// <summary>
		/// Gets the current <see cref="IRelativityFacade"/> instance.
		/// </summary>
		public IRelativityFacade Facade { get; private set; }

		public IRelativityFacade SetUpFacade()
		{
			// Code Smell - Can we remove the internal Facade function of TestSession? RTF-956
			// Code Smell - Can we remove the internal RelativityFacade constructor? RTF-957
			return TestSession.Current.Facade = Facade = new RelativityFacade();
		}

		public void TearDownFacade()
		{
			if (Facade != null)
			{
				Facade.Dispose();
				Facade = null;
			}
		}

		public IRelativityFacade AddCoreToFacade()
		{
			IConfigurationRoot configurationRoot = new ConfigurationBuilder().
				SetBasePath(AppDomain.CurrentDomain.BaseDirectory).
				AddEnvironmentVariables().
				AddNUnitParameters().
				AddInMemoryCollection(GetObjectPropertyNameValuePairs(RelativityInstanceConfiguration)).
				Build();

			return Facade.RelyOn(new CoreComponent { ConfigurationRoot = configurationRoot });
		}

		public IRelativityFacade AddApiToFacade()
		{
			return Facade.RelyOn<ApiComponent>();
		}

		public IRelativityFacade SetUpFacadeWithCore()
		{
			SetUpFacade();

			return AddCoreToFacade();
		}

		public IRelativityFacade SetUpFacadeWithCoreAndApi()
		{
			SetUpFacadeWithCore();

			return AddApiToFacade();
		}

		private static IEnumerable<KeyValuePair<string, string>> GetObjectPropertyNameValuePairs(object source)
		{
			var properties = source.GetType().GetProperties(BindingFlags.Public | BindingFlags.GetProperty | BindingFlags.SetProperty | BindingFlags.Instance);

			return properties.Select(x => new KeyValuePair<string, string>(x.Name, x.GetValue(source)?.ToString()));
		}
	}
}
