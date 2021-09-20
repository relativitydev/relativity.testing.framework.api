using System.Collections.Generic;
using System.Linq;
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
			_mockTelemetryClient = new Mock<IApplicationInsightsTelemetryClient>();

			_unit = new ApplicationInsightsMetricInterceptor(new Mock<IRelativityFacade>().Object);
		}

		[TestCaseSource(nameof(_dataCollectionStateToExpectedProperties))]
		public void Ensure_CollectedProperties_RespectDataCollectionState(
	DataCollection testCollectionState, IEnumerable<string> expectedPropertyKeys)
		{
			_unit.CollectionState = testCollectionState;

			_unit.Intercept(new Mock<IInvocation>().Object);

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

			_unit.Intercept(new Mock<IInvocation>().Object);

			_mockTelemetryClient.Verify(
				x => x.TrackMetric(
					It.IsAny<string>(),
					It.IsAny<double>(),
					It.Is<Dictionary<string, string>>(
						props => !props.Any())), Times.Once);
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
