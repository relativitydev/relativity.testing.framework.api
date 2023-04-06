using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(MatterDeleteByIdStrategyV1))]
	public class MatterDeleteByIdStrategyV1Fixture
	{
		private const int _MATTER_ID = 1;
		private readonly string _deleteUrl = $"relativity-environment/v1/workspaces/-1/matters/{_MATTER_ID}";

		private Mock<IRestService> _mockRestService;
		private Mock<IArtifactIdValidator> _mockArtifactIdValidator;
		private MatterDeleteByIdStrategyV1 _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockArtifactIdValidator = new Mock<IArtifactIdValidator>();

			_sut = new MatterDeleteByIdStrategyV1(
				_mockRestService.Object, _mockArtifactIdValidator.Object);
		}

		[Test]
		public void Delete_WithValidId_ShouldCallRestServiceWithExpectedUrl()
		{
			_sut.Delete(_MATTER_ID);
			_mockRestService.Verify(restService => restService.Delete(_deleteUrl, null, null), Times.Once);
		}

		[Test]
		public void Delete_WithAnyId_ShouldCallArtifactIdValidator()
		{
			_sut.Delete(_MATTER_ID);
			_mockArtifactIdValidator.Verify(validator => validator.Validate(_MATTER_ID, "Matter"), Times.Once);
		}
	}
}
