using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Microsoft.ApplicationInsights;

namespace Relativity.Testing.Framework.Api.Interceptors
{
	public abstract class ApplicationInsightsInterceptor : IInterceptor, IApplicationInsightsInterceptor
	{
		private readonly IRelativityFacade _relativityFacade;
		private readonly string _rtfVersion;
		private readonly string _testAssemblyName;
		private readonly string _ringSetupVersion;

		private bool _enabled;

		protected ApplicationInsightsInterceptor(IRelativityFacade relativityFacade)
		{
#pragma warning disable CS0618 // Maintain existing behavior (auto-property default was true)
			IsEnabled = true;
#pragma warning restore CS0618 // Type or member is obsolete

			_relativityFacade = relativityFacade;

			TelemetryClient = _relativityFacade.Resolve<IApplicationInsightsTelemetryClient>();

			_rtfVersion = Assembly.GetAssembly(typeof(ApplicationInsightsInterceptor)).GetName().Version.ToString();

			string executingAssemblyDomainName = AppDomain.CurrentDomain.FriendlyName;
			_testAssemblyName = ConvertAppDomainToAssemblyName(executingAssemblyDomainName);

			Assembly executingAssembly = GetAssemblyFromAppDomainOrNull(_testAssemblyName);
			_ringSetupVersion = (executingAssembly != null) ? GetRingSetupVersionReferencedInAssembly(executingAssembly) : null;
		}

		protected IApplicationInsightsTelemetryClient TelemetryClient { get; set; }

		/// <summary>
		/// Gets or sets a value indicating whether or not DataCollection.All is configured for this interceptor.
		/// </summary>
		/// <remarks>
		/// This will return 'false' when CollectionState is 'UsageOnly', this is to avoid
		/// any external client violating that state (by incorrectly treating configuration as 'All').
		/// </remarks>
		[Obsolete("This property exists to maintain any backwards compatibility; update to use CollectionState.")]
		public bool IsEnabled
		{
			get
			{
				return CollectionState == DataCollection.All;
			}

			set
			{
				// Only here to maintain backwards compilability.
				// Anything setting this value (other than us) shouldn't have been.
				// Should this just be gutted now then? Clients control it with 'EnableApplicationInsights'
				_enabled = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating what type of data is being collected.
		/// </summary>
		public DataCollection CollectionState { get; set; } = DataCollection.UsageOnly;

		public abstract void Intercept(IInvocation invocation);

		protected Dictionary<string, string> BuildInvocationProperties(IInvocation invocation)
		{
			var properties = new Dictionary<string, string>();

			switch (CollectionState)
			{
				case DataCollection.None:
					break;
				case DataCollection.UsageOnly:
					properties = new Dictionary<string, string>
					{
						{ "RelativityVersion", _relativityFacade.RelativityInstanceVersion },
						{ "Class", invocation.TargetType.Name },
						{ "Method", invocation.Method.Name },
						{ "RelativityTestingFrameworkVersion", _rtfVersion },
						{ "RingSetupVersion", _ringSetupVersion },
						{ "Hostname", _relativityFacade?.Config?.RelativityInstance?.RelativityHostAddress }
					};
					break;
				case DataCollection.All:
					properties = new Dictionary<string, string>
					{
						{ "RelativityVersion", _relativityFacade.RelativityInstanceVersion },
						{ "Class", invocation.TargetType.Name },
						{ "Method", invocation.Method.Name },
						{ "Parameters", string.Join(" && ", invocation.Arguments.Where(x => x != null)) },
						{ "RelativityTestingFrameworkVersion", _rtfVersion },
						{ "TestAssemblyName", _testAssemblyName },
						{ "RingSetupVersion", _ringSetupVersion },
						{ "Hostname", _relativityFacade?.Config?.RelativityInstance?.RelativityHostAddress }
					};
					break;
				default:
					break;
			}

			return properties;
		}

		protected static double DoInvocation(IInvocation invocation)
		{
			var stopwatch = System.Diagnostics.Stopwatch.StartNew();

			invocation.Proceed();

			stopwatch.Stop();

			var processingTime = stopwatch.Elapsed.TotalMilliseconds;

			return processingTime;
		}

		internal static string ConvertAppDomainToAssemblyName(string domainName)
		{
			string dllName = domainName.Split('-').Last();
			string assemblyName = Path.GetFileNameWithoutExtension(dllName);
			return assemblyName;
		}

		internal static string GetRingSetupVersionReferencedInAssembly(Assembly assembly)
		{
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			string ringSetupVersion = referencedAssemblies.FirstOrDefault(x => x.Name == "Relativity.Testing.Framework.RingSetup")?.Version?.ToString();
			return ringSetupVersion;
		}

		internal static Assembly GetAssemblyFromAppDomainOrNull(string assemblyName)
		{
			try
			{
				Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
				Assembly executingAssembly = assemblies.FirstOrDefault(x => x.GetName().Name == assemblyName);
				return executingAssembly;
			}
			catch
			{
				return null;
			}
		}
	}
}
