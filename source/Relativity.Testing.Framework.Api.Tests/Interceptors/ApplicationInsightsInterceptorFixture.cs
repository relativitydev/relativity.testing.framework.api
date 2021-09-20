using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Tests.Utilities;

namespace Relativity.Testing.Framework.Api.Tests
{
	[TestFixture]
	public class ApplicationInsightsInterceptorFixture
	{
		private TestApplicationInsightsInterceptor _unit;

		private Mock<IRelativityFacade> _mockFacade;
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
		[Description("'All' should map to 'True'; everything else should map to 'false'. This avoids legacy consumers violating collection state.")]
		public void EnsurePropertiesAreExpected_DataCollection_IsEnabled([Values] DataCollection testCollectionState)
		{
			bool expectedEnabled = false;
			_unit.CollectionState = testCollectionState;

			if (testCollectionState == DataCollection.All)
			{
				expectedEnabled = true;
			}

#pragma warning disable CS0618 // Intentionally testing old behavior for backwards compatibility.
			bool actualEnabled = _unit.IsEnabled;
#pragma warning restore CS0618 // Type or member is obsolete

			Assert.That(actualEnabled, Is.EqualTo(expectedEnabled));
		}
	}
}
