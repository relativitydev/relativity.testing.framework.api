using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Relativity.Testing.Framework.Configuration;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	/// <summary>
	/// Provides static methods and properties for the configuation reading.
	/// </summary>
	public static class Config
	{
		private static readonly Lazy<IConfigurationService> _lazyConfigurationService = new Lazy<IConfigurationService>(InitializeConfigurationService);

		public static IConfigurationService Service => _lazyConfigurationService.Value;

		private static IConfigurationService InitializeConfigurationService()
		{
			IConfigurationRoot configurationRoot = new ConfigurationBuilder().
				SetBasePath(AppDomain.CurrentDomain.BaseDirectory).
				AddEnvironmentVariables().
				AddNUnitParameters().
				Build();

			// Code Smell - Can we remove the reliance on this internal class? - https://jira.kcura.com/browse/RTF-958
			return new ConfigurationService(configurationRoot);
		}

		public static IEnumerable<string> GetTestRelativityInstanceAliases()
		{
			string aliasesValue = Service.ConfigurationRoot.GetSection("testRelativityInstances").Value;

			if (aliasesValue == "default")
			{
				return Enumerable.Empty<string>();
			}
			else if (aliasesValue == "all")
			{
				return GetRelativityInstanceConfigurations().Keys;
			}
			else
			{
				return aliasesValue?.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(x => x.Trim())
					?? Enumerable.Empty<string>();
			}
		}

		private static Dictionary<string, RelativityInstanceConfiguration> GetRelativityInstanceConfigurations()
		{
			return Service.GetValue<Dictionary<string, RelativityInstanceConfiguration>>("relativityInstances");
		}

		public static RelativityInstanceConfiguration GetRelativityInstanceConfiguration(string alias)
		{
			RelativityInstanceConfiguration defaultConfiguration = Service.RelativityInstance;

			return alias == null
				? defaultConfiguration
				: GetRelativityInstanceConfigurations()[alias].MergeWithDefault(defaultConfiguration);
		}
	}
}
