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
		private readonly bool _usingRingSetup;

		protected ApplicationInsightsInterceptor(IRelativityFacade relativityFacade)
		{
			_relativityFacade = relativityFacade;

			TelemetryClient = _relativityFacade.Resolve<ApplicationInsightsTelemetryClient>().Instance;

			_rtfVersion = Assembly.GetAssembly(typeof(ApplicationInsightsInterceptor)).GetName().Version.ToString();

			string executingAssemblyDomainName = AppDomain.CurrentDomain.FriendlyName;
			_testAssemblyName = ConvertAppDomainToAssemblyName(executingAssemblyDomainName);

			Assembly executingAssembly = GetAssemblyFromAppDomainOrNull(_testAssemblyName);
			_usingRingSetup = (executingAssembly != null) && RingSetupIsReferencedInAssembly(executingAssembly);
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
				{ "AdsCiCd", _usingRingSetup.ToString() }
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

		internal static bool RingSetupIsReferencedInAssembly(Assembly assembly)
		{
			AssemblyName[] referencedAssemblies = assembly.GetReferencedAssemblies();
			bool isReferencingRingSetup = referencedAssemblies.FirstOrDefault(x => x.Name == "Relativity.Testing.Framework.RingSetup") != null;
			return isReferencingRingSetup;
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
