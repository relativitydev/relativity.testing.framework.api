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

		protected ApplicationInsightsInterceptor(IRelativityFacade relativityFacade)
		{
			_relativityFacade = relativityFacade;

			TelemetryClient = _relativityFacade.Resolve<IApplicationInsightsTelemetryClient>().Instance;

			_rtfVersion = Assembly.GetAssembly(typeof(ApplicationInsightsInterceptor)).GetName().Version.ToString();

			string executingAssemblyDomainName = AppDomain.CurrentDomain.FriendlyName;
			_testAssemblyName = ConvertAppDomainToAssemblyName(executingAssemblyDomainName);

			Assembly executingAssembly = GetAssemblyFromAppDomainOrNull(_testAssemblyName);
			_ringSetupVersion = (executingAssembly != null) ? GetRingSetupVersionReferencedInAssembly(executingAssembly) : null;
		}

		protected TelemetryClient TelemetryClient { get; set; }

		public bool IsEnabled { get; set; } = true;

		public abstract void Intercept(IInvocation invocation);

		protected Dictionary<string, string> BuildInvocationProperties(IInvocation invocation)
		{
			var properties = new Dictionary<string, string>
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
