using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Arrangement;
using Relativity.Testing.Framework.Configuration;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	/// <summary>
	/// Represents the base fixture class for API test fixtures.
	/// </summary>
	[TestFixture]
	public abstract class ApiTestFixture : SessionBasedFixture
	{
		protected static readonly object Locker = new object();

		private static readonly Lazy<IConfigurationService> _lazyConfigurationService = new Lazy<IConfigurationService>(InitializeConfigurationService);

		/// <summary>
		/// Initializes a new instance of the <see cref="ApiTestFixture"/> class using default Relativity instance configuration.
		/// </summary>
		protected ApiTestFixture()
		{
			FacadeHost = new RelativityFacadeHost();
		}

		/// <summary>
		/// Gets the <see cref="RelativityFacadeHost"/> instance.
		/// </summary>
		protected RelativityFacadeHost FacadeHost { get; }

		/// <inheritdoc>
		protected override IRelativityFacade Facade => FacadeHost.Facade;

		public static IConfigurationService Service => _lazyConfigurationService.Value;

		/// <summary>
		/// Gets current Relativity instance configuration.
		/// </summary>
		protected RelativityInstanceConfiguration RelativityInstanceConfiguration => FacadeHost.RelativityInstanceConfiguration;

		/// <inheritdoc>
		protected override void OnSetUpFixture()
		{
			base.OnSetUpFixture();

			FacadeHost.SetUpFacadeWithCoreAndApi();
			CheckVersionRangeForFixture();
		}

		/// <inheritdoc>
		protected override void OnSetUpTest()
		{
			base.OnSetUpTest();
			CheckVersionRangeForTest();
		}

		/// <inheritdoc>
		protected override void OnTearDownFixture()
		{
			base.OnTearDownFixture();

			FacadeHost.TearDownFacade();
		}

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
	}
}
