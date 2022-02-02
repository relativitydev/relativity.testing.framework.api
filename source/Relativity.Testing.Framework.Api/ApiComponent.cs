using System;
using System.Linq;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Castle.Windsor.Proxy;
using Polly;
using Polly.Retry;
using Relativity.Testing.Framework.Api.Attributes;
using Relativity.Testing.Framework.Api.Interceptors;
using Relativity.Testing.Framework.Api.Kepler;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Versioning;
using Relativity.Testing.Framework.Configuration;
using Relativity.Testing.Framework.Logging;
using Relativity.Testing.Framework.Models;
using Relativity.Testing.Framework.Versioning;

namespace Relativity.Testing.Framework.Api
{
	/// <summary>
	/// Represents the API component of Relativity Testing Framework.
	/// <see cref="ApiComponent"/> should be registered in [RelativityFacade](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.RelativityFacade.html) thru [IRelativityFacade.RelyOn&lt;T&gt;()](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.IRelativityFacade.html#Relativity_Testing_Framework_IRelativityFacade_RelyOn__1) method after [CoreComponent](https://relativitydev.github.io/relativity.testing.framework/api/Relativity.Testing.Framework.CoreComponent.html).
	/// </summary>
	public class ApiComponent : IRelativityComponent, IWindsorInstaller
	{
		private readonly Type[] _commonInterceptors = new[]
		{
			typeof(RetryInterceptor),
			typeof(ApplicationInsightsEventInterceptor),
			typeof(LoggingInterceptor)
		};

		private IKeplerServiceFactory _serviceFactory;

		/// <summary>
		/// The configuration key of boolean property to check the Relativity version.
		/// </summary>
		public const string PerformRelativityVersionCheckConfigurationKey = "PerformRelativityVersionCheck";

		/// <summary>
		/// The supported Relativity version range.
		/// </summary>
		public const string SupportedRelativityVersionRange = "11.3 - 13.0";

		/// <summary>
		/// Gets the kepler service factory.
		/// Use this method at your own risk. The returned service factory is not supported, and may not work.
		/// </summary>
		/// <value>
		/// The kepler service factory.
		/// </value>
		public IKeplerServiceFactory ServiceFactory => _serviceFactory
			?? (_serviceFactory = RelativityFacade.Instance.Resolve<IKeplerServiceFactory>());

		void IRelativityComponent.Initialize(IWindsorContainer container)
		{
			container.Install(this);
		}

		void IWindsorInstaller.Install(IWindsorContainer container, IConfigurationStore store)
		{
			container.Kernel.ProxyFactory = new DefaultProxyFactory(disableSignedModule: true);

			container.Register(
				Component.For<ApplicationInsightsEventInterceptor>().
				LifestyleSingleton());

			container.Register(
				Component.For<ApplicationInsightsMetricInterceptor>().
				LifestyleSingleton());

			var configurationService = container.ResolveAll<IConfigurationService>().FirstOrDefault();

			if (configurationService != null)
			{
				DataCollection stateToConfigure;
				string defaultValue = DataCollection.All.ToString();
				string configuredValue = configurationService.GetValueOrDefault("EnableApplicationInsights", defaultValue).ToLower();

				switch (configuredValue)
				{
					case "all":
						stateToConfigure = DataCollection.All;
						break;
					case "true":
						stateToConfigure = DataCollection.All;
						break;
					case "usageonly":
						stateToConfigure = DataCollection.UsageOnly;
						break;
					case "none":
						stateToConfigure = DataCollection.None;
						break;
					case "false":
						stateToConfigure = DataCollection.None;
						break;
					default:
						stateToConfigure = DataCollection.All;
						break;
				}

				container.Resolve<ApplicationInsightsEventInterceptor>().CollectionState = stateToConfigure;
				container.Resolve<ApplicationInsightsMetricInterceptor>().CollectionState = stateToConfigure;
			}

			container.Register(
				Component.For<RetryInterceptor>().
				LifestyleSingleton());

			RegisterValidators(container);

			container.Register(
				Component.For<IKeplerServiceFactory>().
				ImplementedBy<KeplerServiceFactory>().
				LifestyleSingleton());

			RegisterServicesAndNonGenericStrategies(container);

			RegisterAllFieldStrategies(container);

			InstallGenericStrategies(container);
		}

		private void RegisterValidators(IWindsorContainer container)
		{
			RegisterClassesByPredicate(container, type => type.Name.Contains("Validator"), typeof(LoggingInterceptor));
		}

		private void RegisterServicesAndNonGenericStrategies(IWindsorContainer container)
		{
			RegisterClassesByPredicate(container, type => type.Name == nameof(RestService) || type.Name == nameof(ApiRelativityInstanceVersionResolveService) || type.Name == nameof(RelativityApplicationVersionResolveService), typeof(LoggingInterceptor));

			var commonInterceptorsWithoutRetry = _commonInterceptors.Where(interceptor => interceptor != typeof(RetryInterceptor)).ToArray();

			RegisterClassesByPredicate(container, BuildServicesAndNonGenericStrategiesWithRetryPredicate(), _commonInterceptors);

			RegisterClassesByPredicate(container, type => TypeHasDoNotRetryAttribute(type), commonInterceptorsWithoutRetry);
		}

		private static void RegisterClassesByPredicate(
			IWindsorContainer container,
			Predicate<Type> typesPredicate,
			params Type[] interceptors)
		{
			container.Register(
				Classes.FromThisAssembly().
					IncludeNonPublicTypes().
					Where(typesPredicate).
					Configure(x => x.Interceptors(interceptors)).
					WithServiceAllInterfaces().
					LifestyleSingleton());
		}

		private static Predicate<Type> BuildServicesAndNonGenericStrategiesWithRetryPredicate()
		{
			return type =>
				(type.Name.EndsWith("Service") || (type.Name.Contains("Strategy") && !type.IsGenericType)) &&
				!TypeHasDoNotRetryAttribute(type);
		}

		private static bool TypeHasDoNotRetryAttribute(Type type)
		{
			return type.GetCustomAttributes(typeof(DoNotRetryAttribute), true).Any();
		}

		private void RegisterInterceptors(ComponentRegistration componentRegistration)
		{
			componentRegistration.Interceptors(_commonInterceptors);
		}

		private void RegisterAllFieldStrategies(IWindsorContainer container)
		{
			RegisterFieldStrategies(container);

			container.Register(Component.For(typeof(IGetWorkspaceEntityByIdStrategy<Field>))
				.ImplementedBy(typeof(FieldGetByIdStrategy<Field>))
				.Interceptors(_commonInterceptors));

			container.Register(Component.For(typeof(IGetWorkspaceEntityByNameStrategy<Field>))
				.ImplementedBy(typeof(FieldGetByNameStrategy<Field>))
				.Interceptors(_commonInterceptors));
		}

		private void RegisterFieldStrategies(IWindsorContainer container)
		{
			container.Register(Component
				.For(typeof(IGetWorkspaceEntityByIdStrategy<>))
				.ImplementedBy(typeof(FieldGetByIdStrategy<>))
				.Interceptors(_commonInterceptors));

			container.Register(Component
				.For(typeof(IGetWorkspaceEntityByNameStrategy<>))
				.ImplementedBy(typeof(FieldGetByNameStrategy<>))
				.Interceptors(_commonInterceptors));

			container.Register(Component
				.For(typeof(ICreateWorkspaceEntityStrategy<>))
				.ImplementedBy(typeof(FieldCreateStrategy<>))
				.Interceptors(_commonInterceptors));

			container.Register(Component
				.For(typeof(IUpdateWorkspaceEntityStrategy<>))
				.ImplementedBy(typeof(FieldUpdateStrategy<>))
				.Interceptors(_commonInterceptors));

			container.Register(Component
				.For(typeof(IRequireWorkspaceEntityStrategy<>))
				.ImplementedBy(typeof(FieldRequireStrategy<>))
				.Interceptors(_commonInterceptors));
		}

		private void InstallGenericStrategies(IWindsorContainer container)
		{
			Type[] types = new[]
			{
				typeof(ObjectQueryExistsByIdStrategy<User>),
				typeof(EnsureExistsByIdStrategy<User>),

				typeof(ObjectQueryExistsByIdStrategy<Group>),
				typeof(EnsureExistsByIdStrategy<Group>),

				typeof(WorkspaceObjectExistsByIdStrategy<Choice>),
				typeof(EnsureWorkspaceEntityExistsByIdStrategy<Choice>),

				typeof(EnsureExistsByIdStrategy<Workspace>)
			};

			container.Register(
				Classes.From(types).
					Pick().
					Configure(RegisterInterceptors).
					WithServiceAllInterfaces().
					LifestyleSingleton());
		}

		void IRelativityComponent.Ensure(IWindsorContainer container)
		{
			EnsureConfiguration(container.Resolve<IConfigurationService>(), container);
			RetryPolicy policy = Policy
				.Handle<RelativityComponentEnsuringException>()
				.WaitAndRetry(new[]
				{
					TimeSpan.FromSeconds(5),
					TimeSpan.FromSeconds(30),
					TimeSpan.FromSeconds(60)
				});

			policy.Execute(() => EnsureRestServicesHost(container.Resolve<IRestService>()));

			string relativityVersion = container.Resolve<IRelativityFacade>().RelativityInstanceVersion;
			IVersionRangeMatchService versionRangeMatchService = container.Resolve<IVersionRangeMatchService>();

			bool performRelativityVersionCheck = container.Resolve<IConfigurationService>().
				GetValueOrDefault(PerformRelativityVersionCheckConfigurationKey, true);

			if (performRelativityVersionCheck)
			{
				EnsureRelativityVersion(relativityVersion, versionRangeMatchService);
			}
		}

		private void EnsureConfiguration(IConfigurationService configurationService, IWindsorContainer container)
		{
			var instanceConfiguration = configurationService.RelativityInstance;

			if (string.IsNullOrEmpty(instanceConfiguration.AdminUsername))
				throw new ConfigurationKeyNotFoundException(nameof(RelativityInstanceConfiguration.AdminUsername));

			if (string.IsNullOrEmpty(instanceConfiguration.AdminPassword))
				throw new ConfigurationKeyNotFoundException(nameof(RelativityInstanceConfiguration.AdminPassword));

			if (string.IsNullOrEmpty(instanceConfiguration.ServerBindingType))
				throw new ConfigurationKeyNotFoundException(nameof(RelativityInstanceConfiguration.ServerBindingType));

			if (string.IsNullOrEmpty(instanceConfiguration.RestServicesHostAddress))
				throw new ConfigurationKeyNotFoundException(nameof(RelativityInstanceConfiguration.RestServicesHostAddress));

			var relativityVersion = container.Resolve<IRelativityFacade>().RelativityInstanceVersion;
			var versionRangeMatchService = container.Resolve<IVersionRangeMatchService>();

			if (versionRangeMatchService.IsVersionInRange(relativityVersion, "<11.3") && string.IsNullOrEmpty(instanceConfiguration.RsapiServicesHostAddress))
				throw new ConfigurationKeyNotFoundException(nameof(RelativityInstanceConfiguration.RsapiServicesHostAddress));
		}

		private void EnsureRestServicesHost(IRestService restService)
		{
			string response;

			try
			{
				response = restService.Post<string>("Relativity.Services.Environmental.IEnvironmentModule/Ping Service/Ping");
			}
			catch (Exception exception)
			{
				throw new RelativityComponentEnsuringException(BuildRestServicesHostErrorMessage(restService), exception);
			}

			const string expectedResponse = "\"OK\"";

			if (!string.Equals(response, expectedResponse, StringComparison.OrdinalIgnoreCase))
			{
				throw new RelativityComponentEnsuringException(BuildRestServicesHostErrorMessage(restService));
			}
		}

		private static string BuildRestServicesHostErrorMessage(IRestService restService)
		{
			return $@"Unable to ensure that Relativity REST services host is accessible.
	Attempted to connect to {restService.BaseUrl} with {restService.Username} user.
	Check to make sure the remote name is the proper value for the IIS settings on the machine.
	Check the RelativityLogs and Event Viewer for more information on why Service Host failed to stand up the Relativity.Services.";
		}

		private void EnsureRelativityVersion(string version, IVersionRangeMatchService versionRangeMatchService)
		{
			if (!versionRangeMatchService.IsVersionInRange(version, SupportedRelativityVersionRange))
			{
				RelativityFacade.Instance.Log.Warn(BuildRelativityVersionWarnMessage(version));
			}
		}

		private static string BuildRelativityVersionWarnMessage(string version)
		{
			string assemblyName = typeof(ApiComponent).Assembly.GetName().Name;

			return $@"Run {assemblyName} against the version {version} of Relativity instance is outside the list of supported RTF versions.
	Supported versions are: {SupportedRelativityVersionRange}.
	If you want to run {assemblyName} against an unofficially supported version of Relativity at your own risk then set {PerformRelativityVersionCheckConfigurationKey} = false configuration option.";
		}
	}
}
