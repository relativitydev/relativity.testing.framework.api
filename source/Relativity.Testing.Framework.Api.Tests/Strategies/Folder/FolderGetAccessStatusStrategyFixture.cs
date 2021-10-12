using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(IFolderGetAccessStatusStrategy))]
	internal class FolderGetAccessStatusStrategyFixture
	{
		private const int _VALID_WORKSPACE_ARTIFACT_ID = 1;
		private const int _VALID_FOLDER_ARTIFACT_ID = 2;

		private const string _GET_URL = "Relativity.Services.Folder.IFolderModule/Folder%20Manager/DeleteUnusedFoldersAsync";

		private FolderAccessStatus _expectedAccessStatus;

		private Mock<IRestService> _mockRestService;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private Mock<IArtifactIdValidator> _mockArtifactIdValidator;
		private IFolderGetAccessStatusStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			var expectedAccessStatusDto = new FolderAccessStatusDto
			{
				CanView = true,
				Exists = true
			};
			_expectedAccessStatus = new FolderAccessStatus
			{
				CanView = expectedAccessStatusDto.CanView,
				Exists = expectedAccessStatusDto.Exists
			};

			_mockRestService.Setup(restService => restService.Post<FolderAccessStatusDto>(_GET_URL, It.IsAny<object>(), 2, null))
				.Returns(expectedAccessStatusDto);
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_mockArtifactIdValidator = new Mock<IArtifactIdValidator>();

			_sut = new FolderGetAccessStatusStrategy(
				_mockRestService.Object,
				_mockWorkspaceIdValidator.Object,
				_mockArtifactIdValidator.Object);
		}

		[Test]
		public void Get_WithAnyParameters_CallsWorkspaceIdValidator()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID);

			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_VALID_WORKSPACE_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Get_WithAnyParameters_CallsArtifactIdValidator()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID);

			_mockArtifactIdValidator.Verify(validator => validator.Validate(_VALID_FOLDER_ARTIFACT_ID, "Folder"), Times.Once);
		}

		[Test]
		public void Get_WithValidParameters_CallsRestService()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID);

			_mockRestService.Verify(restService => restService.Post<QueryResult<Artifact>>(_GET_URL, It.IsAny<object>(), 2, null), Times.Once);
		}

		[Test]
		public void Delete_WithValidWorkspaceArtifactID_ReturnsExpectedResponse()
		{
			FolderAccessStatus response = _sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID);

			response.Should().NotBeNull();
			response.Should().BeEquivalentTo(_expectedAccessStatus);
		}
	}
}
