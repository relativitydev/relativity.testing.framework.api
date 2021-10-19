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
	[TestOf(typeof(IFolderMoveStrategy))]
	internal class FolderMoveStrategyFixture
	{
		private const string _MOVE_URL = "Relativity.Services.Folder.IFolderModule/Folder%20Manager/MoveFolderAsync";
		private const int _VALID_WORKSPACE_ARTIFACT_ID = 1;
		private const int _VALID_FOLDER_ARTIFACT_ID = 2;
		private const int _VALID_DESTINATION_FOLDER_ARTIFACT_ID = 3;

		private FolderMoveResponse _expectedMoveResponse;

		private Mock<IRestService> _mockRestService;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private Mock<IArtifactIdValidator> _mockArtifactIdValidator;
		private IFolderMoveStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			SetupMockRestService();
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_mockArtifactIdValidator = new Mock<IArtifactIdValidator>();

			_sut = new FolderMoveStrategy(
				_mockRestService.Object,
				_mockWorkspaceIdValidator.Object,
				_mockArtifactIdValidator.Object);
		}

		[Test]
		public void Move_WithAnyParameters_CallsWorkspaceIdValidator()
		{
			_sut.Move(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID, _VALID_DESTINATION_FOLDER_ARTIFACT_ID);

			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_VALID_WORKSPACE_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Move_WithAnyParameters_CallsArtifactIdValidatorForFolderArtifactId()
		{
			_sut.Move(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID, _VALID_DESTINATION_FOLDER_ARTIFACT_ID);

			_mockArtifactIdValidator.Verify(validator => validator.Validate(_VALID_FOLDER_ARTIFACT_ID, "Folder"), Times.Once);
		}

		[Test]
		public void Move_WithAnyParameters_CallsArtifactIdValidatorForDestinationFolderArtifactId()
		{
			_sut.Move(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID, _VALID_DESTINATION_FOLDER_ARTIFACT_ID);

			_mockArtifactIdValidator.Verify(validator => validator.Validate(_VALID_DESTINATION_FOLDER_ARTIFACT_ID, "Destination Folder"), Times.Once);
		}

		[Test]
		public void Move_WithAnyParameters_CallsRestService()
		{
			_sut.Move(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID, _VALID_DESTINATION_FOLDER_ARTIFACT_ID);

			_mockRestService.Verify(restService => restService.Post<FolderMoveResponseDto>(_MOVE_URL, It.IsAny<object>(), 2, null), Times.Once);
		}

		[Test]
		public void Move_WithValidParameters_ReturnsExpectedResponse()
		{
			FolderMoveResponse response = _sut.Move(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID, _VALID_DESTINATION_FOLDER_ARTIFACT_ID);

			response.Should().NotBeNull();
			response.Should().BeEquivalentTo(_expectedMoveResponse);
		}

		private void SetupMockRestService()
		{
			_mockRestService = new Mock<IRestService>();
			var moveResponseDto = new FolderMoveResponseDto
			{
				ProcessState = "Test Process State",
				TotalOperations = 3,
				OperationsCompleted = 2
			};
			_expectedMoveResponse = new FolderMoveResponse
			{
				ProcessState = moveResponseDto.ProcessState,
				TotalOperations = moveResponseDto.TotalOperations,
				OperationsCompleted = moveResponseDto.OperationsCompleted
			};
			_mockRestService.Setup(restService => restService.Post<FolderMoveResponseDto>(_MOVE_URL, It.IsAny<object>(), 2, null)).Returns(moveResponseDto);
		}
	}
}
