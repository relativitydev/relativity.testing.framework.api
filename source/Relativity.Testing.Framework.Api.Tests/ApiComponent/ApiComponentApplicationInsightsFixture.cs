using Castle.MicroKernel;
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
	public class ApiComponentApplicationInsightsFixture
	{
		private const string _CONFIGURATION_KEY = "EnableApplicationInsights";

		private IWindsorInstaller _unit;

		private ApplicationInsightsEventInterceptor _realEventInterceptor;
		private ApplicationInsightsMetricInterceptor _realMetricInterceptor;

		// Injected mocks
		private Mock<IWindsorContainer> _mockContainer;

		// Indirect mocks (resolved by container; resolves 'EnableApplicationInsights')
		private Mock<IConfigurationService> _mockConfigurationService;

		// Ignored mocks (resolved/required, but irrelevant to this suite)
		private Mock<IRelativityFacade> _mockFacade;
		private Mock<IConfigurationStore> _mockConfigurationStore;
		private Mock<IApplicationInsightsTelemetryClient> _mockApplicationInsightsClient;
		private Mock<IKernel> _mockKernel;

		[SetUp]
		public void SetUp()
		{
			_mockFacade = new Mock<IRelativityFacade>();
			_mockContainer = new Mock<IWindsorContainer>();
			_mockKernel = new Mock<IKernel>();
			_mockConfigurationStore = new Mock<IConfigurationStore>();
			_mockConfigurationService = new Mock<IConfigurationService>();
			_mockApplicationInsightsClient = new Mock<IApplicationInsightsTelemetryClient>();

			// Shared arrange (white-box/doc mocking)
			IConfigurationService[] singleObjectArray = { _mockConfigurationService.Object };

			_mockFacade
				.Setup(x => x.Resolve<IApplicationInsightsTelemetryClient>())
				.Returns(_mockApplicationInsightsClient.Object);

			_mockContainer
				.Setup(x => x.Kernel)
				.Returns(_mockKernel.Object);

			_mockContainer
				.Setup(x => x.ResolveAll<IConfigurationService>())
				.Returns(singleObjectArray);

			// This whole fixture is concerned with:
			// "When I put '<testvalue>' in 'EnableApplicationInsights',
			// interceptor 'IsEnabled' matches the expected DataCollection state."
			// These classes are where we observe the results of acting.
			// These need to be instantiated after the setup for resolving IConfigurationService
			_realEventInterceptor = new ApplicationInsightsEventInterceptor(_mockFacade.Object);
			_realMetricInterceptor = new ApplicationInsightsMetricInterceptor(_mockFacade.Object);

			// These need to be setup after instantiating the realInterceptors
			_mockContainer
				.Setup(x => x.Resolve<ApplicationInsightsEventInterceptor>())
				.Returns(_realEventInterceptor);
			_mockContainer
				.Setup(x => x.Resolve<ApplicationInsightsMetricInterceptor>())
				.Returns(_realMetricInterceptor);

			// This is the class that acts.
			_unit = new ApiComponent();
		}

		[TestCase("True", DataCollection.All)]
		[TestCase("False", DataCollection.None)]
		[TestCase("All", DataCollection.All)]
		[TestCase("UsageOnly", DataCollection.UsageOnly)]
		[TestCase("None", DataCollection.None)]
		[TestCase("AnythingElse", DataCollection.All)]
		public void WhenValueExists_Install_EventInterceptorInExpectedState(
			string testValue,
			DataCollection expectedState)
		{
			_mockConfigurationService
				.Setup(x => x.GetValueOrDefault(_CONFIGURATION_KEY, It.IsAny<string>()))
				.Returns(testValue);

			_unit.Install(_mockContainer.Object, _mockConfigurationStore.Object);
			DataCollection actualEventInterceptorState = _realEventInterceptor.CollectionState;

			Assert.That(actualEventInterceptorState, Is.EqualTo(expectedState));
		}

		[TestCase("True", DataCollection.All)]
		[TestCase("False", DataCollection.None)]
		[TestCase("All", DataCollection.All)]
		[TestCase("UsageOnly", DataCollection.UsageOnly)]
		[TestCase("None", DataCollection.None)]
		[TestCase("AnythingElse", DataCollection.All)]
		public void WhenValueExists_Install_MetricInterceptorInExpectedState(
			string testValue,
			DataCollection expectedState)
		{
			_mockConfigurationService
				.Setup(x => x.GetValueOrDefault(_CONFIGURATION_KEY, It.IsAny<string>()))
				.Returns(testValue);

			_unit.Install(_mockContainer.Object, _mockConfigurationStore.Object);
			DataCollection actualMetricInterceptorState = _realMetricInterceptor.CollectionState;

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
				.Setup(x => x.GetValueOrDefault(_CONFIGURATION_KEY, It.IsAny<string>()))
				.Returns(testValue);

			_unit.Install(_mockContainer.Object, _mockConfigurationStore.Object);
			DataCollection actualMetricInterceptorState = _realMetricInterceptor.CollectionState;

			Assert.That(actualMetricInterceptorState, Is.EqualTo(DataCollection.All));
		}

		[TestCase("usageonly")]
		[TestCase("usageOnly")]
		[TestCase("UsageOnly")]
		[TestCase("USAGEONLY")]
		public void ConfigurationCasingIsIrrelevant_Install_WhenUsageOnly(string testValue)
		{
			_mockConfigurationService
				.Setup(x => x.GetValueOrDefault(_CONFIGURATION_KEY, It.IsAny<string>()))
				.Returns(testValue);

			_unit.Install(_mockContainer.Object, _mockConfigurationStore.Object);
			DataCollection actualMetricInterceptorState = _realMetricInterceptor.CollectionState;

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
				.Setup(x => x.GetValueOrDefault(_CONFIGURATION_KEY, It.IsAny<string>()))
				.Returns(testValue);

			_unit.Install(_mockContainer.Object, _mockConfigurationStore.Object);
			DataCollection actualMetricInterceptorState = _realMetricInterceptor.CollectionState;

			Assert.That(actualMetricInterceptorState, Is.EqualTo(DataCollection.None));
		}
	}
}
