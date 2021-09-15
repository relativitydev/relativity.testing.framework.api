using System.Collections.Generic;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Interceptors;
using Relativity.Testing.Framework.Configuration;

namespace Relativity.Testing.Framework.Api.Tests
{
	[TestFixture]
	public class ApiComponentApplicationInsightsTests
	{
		private const string _CONFIGURATION_KEY = "EnableApplicationInsights";

		private IWindsorInstaller _unit;

		private ApplicationInsightsEventInterceptor _realEventInterceptor;
		private ApplicationInsightsMetricInterceptor _realMetricInterceptor;

		// Injected mocks
		private Mock<IWindsorContainer> _mockContainer;
		private Mock<IConfigurationStore> _mockConfigurationStore;

		// Indirect mocks (resolved by container)
		private Mock<IConfigurationService> _mockConfigurationService;

		// Ignored mocks
		private Mock<IRelativityFacade> _mockFacade;

		[SetUp]
		public void SetUp()
		{
			_mockFacade = new Mock<IRelativityFacade>();
			_mockContainer = new Mock<IWindsorContainer>();
			_mockConfigurationStore = new Mock<IConfigurationStore>();
			_mockConfigurationService = new Mock<IConfigurationService>();

			// Shared arrange (white-box mocking)
			// This whole fixture is concerned with:
			// "When I put '<testvalue>' in 'EnableApplicationInsights',
			// interceptor 'IsEnabled' matches the expected DataCollection state."
			_realEventInterceptor = new ApplicationInsightsEventInterceptor(_mockFacade.Object);
			_realMetricInterceptor = new ApplicationInsightsMetricInterceptor(_mockFacade.Object);

			IConfigurationService[] singleObjectArray = { _mockConfigurationService.Object };
			_mockContainer
				.Setup(x => x.ResolveAll<IConfigurationService>())
				.Returns(singleObjectArray);
			_mockContainer
				.Setup(x => x.Resolve<ApplicationInsightsEventInterceptor>())
				.Returns(_realEventInterceptor);
			_mockContainer
				.Setup(x => x.Resolve<ApplicationInsightsMetricInterceptor>())
				.Returns(_realMetricInterceptor);

			_unit = new ApiComponent();
		}

		[TestCase("True", DataCollection.All)]
		[TestCase("False", DataCollection.None)]
		[TestCase("All", DataCollection.All)]
		[TestCase("UsageOnly", DataCollection.UsageOnly)]
		[TestCase("None", DataCollection.None)]
		[TestCase("AnythingElse", DataCollection.None)]
		public void WhenValueExists_Install_EventInterceptorInExpectedState(
			string testValue,
			DataCollection expectedState)
		{
			_mockConfigurationService
				.Setup(x => x.GetValueOrDefault(_CONFIGURATION_KEY))
				.Returns(testValue);

			_unit.Install(_mockContainer.Object, _mockConfigurationStore.Object);
			var actualEventInterceptorState = _realEventInterceptor.IsEnabled;

			Assert.That(actualEventInterceptorState, Is.EqualTo(expectedState));
		}

		[TestCase("True", DataCollection.All)]
		[TestCase("False", DataCollection.None)]
		[TestCase("All", DataCollection.All)]
		[TestCase("UsageOnly", DataCollection.UsageOnly)]
		[TestCase("None", DataCollection.None)]
		[TestCase("AnythingElse", DataCollection.None)]
		public void WhenValueExists_Install_MetricInterceptorInExpectedState(
			string testValue,
			DataCollection expectedState)
		{
			_mockConfigurationService
				.Setup(x => x.GetValueOrDefault(_CONFIGURATION_KEY))
				.Returns(testValue);

			_unit.Install(_mockContainer.Object, _mockConfigurationStore.Object);
			var actualMetricInterceptorState = _realMetricInterceptor.IsEnabled;

			Assert.That(actualMetricInterceptorState, Is.EqualTo(expectedState));
		}

		[TestCase("true")]
		[TestCase("True")]
		[TestCase("TRUE")]
		[TestCase("all")]
		[TestCase("All")]
		[TestCase("ALL")]
		public void ConfigurationCasingIsIrrelevant_Install_WhenAll(string testValue)
		{
			_mockConfigurationService
				.Setup(x => x.GetValueOrDefault(_CONFIGURATION_KEY))
				.Returns(testValue);

			_unit.Install(_mockContainer.Object, _mockConfigurationStore.Object);
			var actualMetricInterceptorState = _realMetricInterceptor.IsEnabled;

			Assert.That(actualMetricInterceptorState, Is.EqualTo(DataCollection.All));
		}

		[TestCase("usageonly")]
		[TestCase("usageOnly")]
		[TestCase("UsageOnly")]
		[TestCase("USAGEONLY")]
		public void ConfigurationCasingIsIrrelevant_Install_WhenUsageOnly(string testValue)
		{
			_mockConfigurationService
				.Setup(x => x.GetValueOrDefault(_CONFIGURATION_KEY))
				.Returns(testValue);

			_unit.Install(_mockContainer.Object, _mockConfigurationStore.Object);
			var actualMetricInterceptorState = _realMetricInterceptor.IsEnabled;

			Assert.That(actualMetricInterceptorState, Is.EqualTo(DataCollection.UsageOnly));
		}

		[TestCase("false")]
		[TestCase("False")]
		[TestCase("FALSE")]
		[TestCase("none")]
		[TestCase("None")]
		[TestCase("NONE")]
		public void ConfigurationCasingIsIrrelevant_Install_WhenNone(string testValue)
		{
			_mockConfigurationService
				.Setup(x => x.GetValueOrDefault(_CONFIGURATION_KEY))
				.Returns(testValue);

			_unit.Install(_mockContainer.Object, _mockConfigurationStore.Object);
			var actualMetricInterceptorState = _realMetricInterceptor.IsEnabled;

			Assert.That(actualMetricInterceptorState, Is.EqualTo(DataCollection.None));
		}
	}
}
