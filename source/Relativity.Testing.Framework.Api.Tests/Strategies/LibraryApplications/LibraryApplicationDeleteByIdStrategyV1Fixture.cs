using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(LibraryApplicationDeleteByIdStrategyV1))]
	public class LibraryApplicationDeleteByIdStrategyV1Fixture
	{
		private const int _APPLICATION_ID = 100000;

		private readonly string _deleteUrl = $"relativity-environment/v1/workspace/-1/libraryapplications/{_APPLICATION_ID}";

		private LibraryApplicationDeleteByIdStrategyV1 _strategy;
		private Mock<IRestService> _mockRestService;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_strategy = new LibraryApplicationDeleteByIdStrategyV1(_mockRestService.Object);
		}

		[Test]
		public void Delete_ShouldCallIRestService()
		{
			_strategy.Delete(_APPLICATION_ID);
			_mockRestService.Verify(restService => restService.Delete(_deleteUrl, null, null), Times.Once);
		}
	}
}
