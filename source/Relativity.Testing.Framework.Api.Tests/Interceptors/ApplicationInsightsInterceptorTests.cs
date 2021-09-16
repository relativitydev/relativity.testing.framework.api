using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Tests.Utilities;

namespace Relativity.Testing.Framework.Api.Tests
{
	[TestFixture]
	public class ApplicationInsightsInterceptorTests
	{
		private TestApplicationInsightsInterceptor _unit;

		private Mock<IRelativityFacade> _mockFacade; // Ignored.
		private Mock<IApplicationInsightsTelemetryClient> _mockApplicationInsightsClient;

		[SetUp]
		public void SetUp()
		{
			_mockFacade = new Mock<IRelativityFacade>();
			_mockApplicationInsightsClient = new Mock<IApplicationInsightsTelemetryClient>();

			_mockFacade
				.Setup(x => x.Resolve<IApplicationInsightsTelemetryClient>())
				.Returns(_mockApplicationInsightsClient.Object);

			_unit = new TestApplicationInsightsInterceptor(_mockFacade.Object);
		}

		[Test]
		public void Ensure_OldBehavior_MatchesNewBehavior([Values] DataCollection testCollectionState)
		{
			_unit.CollectionState = testCollectionState;

			bool expectedEnabled = false;
			switch (testCollectionState)
			{
				case DataCollection.All:
					expectedEnabled = true;
					break;
				case DataCollection.UsageOnly:
					expectedEnabled = false;
					break;
				case DataCollection.None:
					expectedEnabled = false;
					break;
				default:
					break;
			}

#pragma warning disable CS0618 // Intentionally testing old behavior for backwards compatibility.
			bool actualEnabled = _unit.IsEnabled;
#pragma warning restore CS0618 // Type or member is obsolete

			Assert.That(actualEnabled, Is.EqualTo(expectedEnabled));
		}
	}
}
