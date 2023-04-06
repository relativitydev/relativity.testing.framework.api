using System;
using System.Linq;
using System.Reflection;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Interceptors;

namespace Relativity.Testing.Framework.Api.FunctionalTests
{
	[TestFixture]
	public class ApplicationInsightsInterceptorFixture
	{
		[Test]
		public void ConvertAppDomainToAssemblyName_ReturnsAssemblyNameWhenAppDomainContainsDashes()
		{
			string initialDomain = "Relativity.Testing.Framework.Api.FunctionalTests.dll";

			string expectedValue = "Relativity.Testing.Framework.Api.FunctionalTests";
			string actualValue = ApplicationInsightsInterceptor.ConvertAppDomainToAssemblyName(initialDomain);

			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void ConvertAppDomainToAssemblyName_ReturnsAssemblyNameWhenAppDomainContainsNoDashes()
		{
			string initialDomain = "domain-96c99582-Relativity.Testing.Framework.Api.FunctionalTests.dll";

			string expectedValue = "Relativity.Testing.Framework.Api.FunctionalTests";
			string actualValue = ApplicationInsightsInterceptor.ConvertAppDomainToAssemblyName(initialDomain);

			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void GetRingSetupVersionReferencedInAssembly_ReturnsNullWhenNotReferenced()
		{
			Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
			Assembly assembly = assemblies.FirstOrDefault(x => x.GetName().Name == "Relativity.Testing.Framework.Api.FunctionalTests");

			string expectedValue = null;
			string actualValue = ApplicationInsightsInterceptor.GetRingSetupVersionReferencedInAssembly(assembly);

			actualValue.Should().Be(expectedValue);
		}

		[Test]
		public void GetAssemblyFromAppDomainOrNull_ReturnsAssemblyWhenPresent()
		{
			string assemblyName = "Relativity.Testing.Framework.Api.FunctionalTests";

			Assembly expectedValue = Assembly.GetExecutingAssembly();
			Assembly actualValue = ApplicationInsightsInterceptor.GetAssemblyFromAppDomainOrNull(assemblyName);

			actualValue.GetName().Name.Should().Be(expectedValue.GetName().Name);
		}

		[Test]
		public void GetAssemblyFromAppDomainOrNull_ReturnsNullWhenNotPresent()
		{
			string assemblyName = "Relativity.Testing.Framework.RingSetup";

			Assembly actualValue = ApplicationInsightsInterceptor.GetAssemblyFromAppDomainOrNull(assemblyName);

			actualValue.Should().BeNull();
		}
	}
}
