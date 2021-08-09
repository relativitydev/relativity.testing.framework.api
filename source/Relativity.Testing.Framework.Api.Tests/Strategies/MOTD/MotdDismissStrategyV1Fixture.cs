using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Configuration;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(MotdDismissStrategyV1))]
	public class MotdDismissStrategyV1Fixture
	{
		private readonly User _mockedUser = new User
		{
			ArtifactID = 1234567
		};

		private IMotdDismissStrategy _motdDismissStrategy;
		private Mock<IRestService> _mockRestService;
		private Mock<IConfigurationService> _mockConfigurationService;
		private Mock<IUserGetByEmailStrategy> _mockUserGetByEmailStrategy;

		[OneTimeSetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockConfigurationService = new Mock<IConfigurationService>();
			_mockUserGetByEmailStrategy = new Mock<IUserGetByEmailStrategy>();

			_mockUserGetByEmailStrategy.Setup(x => x.Get(It.IsAny<string>())).Returns(_mockedUser);

			_motdDismissStrategy = new MotdDismissStrategyV1(_mockConfigurationService.Object, _mockRestService.Object, _mockUserGetByEmailStrategy.Object);
		}

		[Test]
		public void DismissByString_PassesArtifactIDTo_DismissByInt()
		{
			_motdDismissStrategy.Dismiss("AUser@test.com");

			_mockRestService.Verify(mock => mock.Post(It.Is<string>(x => x == $"relativity-infrastructure/v1/workspaces/-1/notifications/dismiss/{_mockedUser.ArtifactID}"), null, null), Times.Once);
		}
	}
}
