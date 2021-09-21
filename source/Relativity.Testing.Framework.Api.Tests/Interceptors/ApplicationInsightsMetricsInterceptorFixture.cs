using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Castle.DynamicProxy;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Interceptors;

namespace Relativity.Testing.Framework.Api.Tests.Interceptors
{
	[TestFixture]
	public class ApplicationInsightsMetricsInterceptorFixture
	{
		private ApplicationInsightsMetricInterceptor _unit;

		private Mock<IInvocation> _mockInvocation;
		private Mock<IRelativityFacade> _mockFacade;
		private Mock<IApplicationInsightsTelemetryClient> _mockTelemetryClient;

		private static readonly IEnumerable<TestCaseData> _dataCollectionStateToExpectedProperties =
	new List<TestCaseData>
	{
				new TestCaseData(
					DataCollection.All,
					new List<string>
					{
						"RelativityVersion",
						"Class",
						"Method",
						"Parameters",
						"RelativityTestingFrameworkVersion",
						"TestAssemblyName",
						"RingSetupVersion",
						"Hostname"
					}),
				new TestCaseData(
					DataCollection.UsageOnly,
					new List<string>
					{
						"RelativityVersion",
						"Class",
						"Method",
						"RelativityTestingFrameworkVersion",
						"RingSetupVersion",
						"Hostname"
					})

				// None gets its own test, see 'Ensure_NoProperties_WhenDataCollectionIsNone'
	};

		[SetUp]
		public void SetUp()
		{
			Type testType = GetType();
			MethodInfo testMethod = testType.GetMethod("SetUp");

			_mockInvocation = new Mock<IInvocation>();
			_mockFacade = new Mock<IRelativityFacade>();
			_mockTelemetryClient = new Mock<IApplicationInsightsTelemetryClient>();

			_mockInvocation.Setup(x => x.TargetType).Returns(testType);
			_mockInvocation.Setup(x => x.Method).Returns(testMethod);

			_mockFacade
				.Setup(x => x.Resolve<IApplicationInsightsTelemetryClient>())
				.Returns(_mockTelemetryClient.Object);

			_unit = new ApplicationInsightsMetricInterceptor(_mockFacade.Object);
		}

		[TestCaseSource(nameof(_dataCollectionStateToExpectedProperties))]
		public void Ensure_CollectedProperties_RespectDataCollectionState(
	DataCollection testCollectionState, IEnumerable<string> expectedPropertyKeys)
		{
			_unit.CollectionState = testCollectionState;

			_unit.Intercept(_mockInvocation.Object);

			_mockTelemetryClient.Verify(x => x.TrackMetric(
				It.IsAny<string>(),
				It.IsAny<double>(),
				It.Is<Dictionary<string, string>>(
					actualProps => OnlyExpected(expectedPropertyKeys, actualProps))));
		}

		[Test]
		public void Ensure_NoProperties_WhenDataCollectionIsNone()
		{
			_unit.CollectionState = DataCollection.None;

			_unit.Intercept(_mockInvocation.Object);

			_mockTelemetryClient.Verify(
				x => x.TrackMetric(
					It.IsAny<string>(),
					It.IsAny<double>(),
					It.IsAny<Dictionary<string, string>>()),
				Times.Never);
		}

		private bool OnlyExpected(IEnumerable<string> expectedKeys, Dictionary<string, string> actualProps)
		{
			IEnumerable<string> actualKeys = actualProps.Keys.Select(x => x);
			IEnumerable<string> sentButNotExpected = actualKeys.Except(expectedKeys);
			IEnumerable<string> expectedButNotSent = expectedKeys.Except(actualKeys);

			// Sent keys should match testing exactly.
			bool exactMatch = !sentButNotExpected.Any() && !expectedButNotSent.Any();

			return exactMatch;
		}
	}
}
