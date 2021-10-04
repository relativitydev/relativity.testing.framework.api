using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(GroupDeleteByIdStrategyV1))]
	internal class GroupDeleteByIdStrategyV1Fixture
	{
		private const int _VALID_GROUP_ARTIFACT_ID = 1;
		private readonly string _deleteUrl = $"Relativity-Identity/v1/groups/{_VALID_GROUP_ARTIFACT_ID}";

		private Mock<IRestService> _mockRestService;
		private Mock<IEnsureExistsByIdStrategy<Group>> _mockEnsureExistsByIdStrategy;
		private GroupDeleteByIdStrategyV1 _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockEnsureExistsByIdStrategy = new Mock<IEnsureExistsByIdStrategy<Group>>();
			_sut = new GroupDeleteByIdStrategyV1(_mockRestService.Object, _mockEnsureExistsByIdStrategy.Object);
		}

		[Test]
		public void Delete_WithAnyParameters_CallsEnsureExistsByIdStrategy()
		{
			_sut.Delete(_VALID_GROUP_ARTIFACT_ID);

			_mockEnsureExistsByIdStrategy.Verify(
				ensureStrategy => ensureStrategy.EnsureExists(_VALID_GROUP_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Delete_WithAnyParameters_CallsRestServiceWithExpectedUrl()
		{
			_sut.Delete(_VALID_GROUP_ARTIFACT_ID);

			_mockRestService.Verify(
				restService => restService.Delete(_deleteUrl, null, null), Times.Once);
		}
	}
}
