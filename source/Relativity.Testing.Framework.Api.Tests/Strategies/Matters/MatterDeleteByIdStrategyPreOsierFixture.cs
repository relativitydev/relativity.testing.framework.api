using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(MatterDeleteByIdStrategyPreOsier))]
	public class MatterDeleteByIdStrategyPreOsierFixture
	{
		private const int _MATTER_ID = 1;
		private readonly string _deleteUrl = "Relativity.Services.Matter.IMatterModule/Matter%20Manager/DeleteSingleAsync";

		private Mock<IRestService> _mockRestService;
		private MatterDeleteByIdStrategyPreOsier _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();

			_sut = new MatterDeleteByIdStrategyPreOsier(_mockRestService.Object);
		}

		[Test]
		public void Delete_WithValidId_ShouldCallRestServiceWithExpectedUrl()
		{
			_sut.Delete(_MATTER_ID);
			_mockRestService.Verify(restService => restService.Post(_deleteUrl, It.IsAny<object>(), null), Times.Once);
		}
	}
}
