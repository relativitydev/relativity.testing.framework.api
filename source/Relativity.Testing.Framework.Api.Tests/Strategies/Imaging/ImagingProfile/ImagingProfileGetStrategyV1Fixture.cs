using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(ImagingProfileGetStrategyV1))]
	public class ImagingProfileGetStrategyV1Fixture
	{
		private const int WorkspaceId = 100000;
		private const int ImagingProfileId = 100000;

		private ImagingProfileGetStrategyV1 _sut;
		private Mock<IRestService> _mockRestService;
		private Mock<IWorkspaceIdValidator> _workspaceIdValidator;
		private Mock<IArtifactIdValidator> _artifactIdValidator;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_workspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_artifactIdValidator = new Mock<IArtifactIdValidator>();

			_sut = new ImagingProfileGetStrategyV1(_mockRestService.Object, _workspaceIdValidator.Object, _artifactIdValidator.Object);
		}

		[Test]
		public void Get_WithAnyInput_ShouldCallValidator()
		{
			_sut.Get(WorkspaceId, ImagingProfileId);
			_workspaceIdValidator.Verify(x => x.Validate(It.IsAny<int>()), Times.Once);
			_artifactIdValidator.Verify(x => x.Validate(It.IsAny<int>(), "ImagingProfile"), Times.Once);
		}

		[Test]
		public async Task GetAsync_WithAnyInput_ShouldCallValidator()
		{
			await _sut.GetAsync(WorkspaceId, ImagingProfileId).ConfigureAwait(false);
			_workspaceIdValidator.Verify(x => x.Validate(It.IsAny<int>()), Times.Once);
			_artifactIdValidator.Verify(x => x.Validate(It.IsAny<int>(), "ImagingProfile"), Times.Once);
		}
	}
}
