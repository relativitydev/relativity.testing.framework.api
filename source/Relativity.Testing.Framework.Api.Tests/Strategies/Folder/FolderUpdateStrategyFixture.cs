using System;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Services;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(IFolderUpdateStrategy))]
	internal class FolderUpdateStrategyFixture
	{
		private const string _UPDATE_URL = "Relativity.Services.Folder.IFolderModule/Folder%20Manager/UpdateSingleAsync";
		private const int _VALID_WORKSPACE_ARTIFACT_ID = 1;
		private const int _FOLDER_WITH_PARENT_ARTIFACT_ID = 2;
		private const int _FOLDER_WITHOUT_PARENT_ARTIFACT_ID = 3;
		private const int _PARENT_FOLDER_ARTIFACT_ID = 3;

		private readonly Folder _folderWithParent = new Folder
		{
			ArtifactID = _FOLDER_WITH_PARENT_ARTIFACT_ID,
			Name = "Folder Updated Name",
			ParentFolder = new NamedArtifact
			{
				ArtifactID = _PARENT_FOLDER_ARTIFACT_ID
			}
		};

		private readonly Folder _folderWithoutParent = new Folder
		{
			ArtifactID = _FOLDER_WITHOUT_PARENT_ARTIFACT_ID,
			Name = "Folder Updated Name"
		};

		private Mock<IRestService> _mockRestService;
		private Mock<IFolderGetByIdStrategy> _mockGetByIdStrategy;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private Mock<IArtifactIdValidator> _mockArtifactIdValidator;
		private IFolderUpdateStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockGetByIdStrategy = new Mock<IFolderGetByIdStrategy>();
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_mockArtifactIdValidator = new Mock<IArtifactIdValidator>();

			_sut = new FolderUpdateStrategy(_mockRestService.Object, _mockGetByIdStrategy.Object, _mockWorkspaceIdValidator.Object, _mockArtifactIdValidator.Object);
		}

		[Test]
		public void Update_WithNullFolder_ThrowsArgumentNullException()
		{
			Folder folder = null;

			Assert.Throws<ArgumentNullException>(() => _sut.Update(_VALID_WORKSPACE_ARTIFACT_ID, folder));
		}

		[Test]
		public void Update_WithAnyParams_CallsWorkspaceIdValidator()
		{
			_sut.Update(_VALID_WORKSPACE_ARTIFACT_ID, _folderWithParent);

			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_VALID_WORKSPACE_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Update_WithNotNullFolder_CallsArtifactIdValidator()
		{
			_sut.Update(_VALID_WORKSPACE_ARTIFACT_ID, _folderWithParent);

			_mockArtifactIdValidator.Verify(validator => validator.Validate(_FOLDER_WITH_PARENT_ARTIFACT_ID, "Folder"), Times.Once);
		}

		[Test]
		public void Update_WithValidParameters_CallsRestServiceWithExpectedParameters()
		{
			_sut.Update(_VALID_WORKSPACE_ARTIFACT_ID, _folderWithParent);

			_mockRestService.Verify(restService => restService.Post(_UPDATE_URL, It.Is<FolderRequest>(request => request.WorkspaceArtifactID == _VALID_WORKSPACE_ARTIFACT_ID && request.Model.Name == _folderWithParent.Name && request.Model.ParentFolder.ArtifactID == _folderWithParent.ParentFolder.ArtifactID), null), Times.Once);
		}

		[Test]
		public void Update_WithValidParametersWithParentFolder_CallsGetFolderByIdStrastegyWithExpectedParameters()
		{
			_sut.Update(_VALID_WORKSPACE_ARTIFACT_ID, _folderWithParent);

			_mockGetByIdStrategy.Verify(getByIdStrategy => getByIdStrategy.Get(_VALID_WORKSPACE_ARTIFACT_ID, _FOLDER_WITH_PARENT_ARTIFACT_ID, _PARENT_FOLDER_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Update_WithValidParametersWithoutParentFolder_CallsGetFolderByIdStrastegyWithExpectedParameters()
		{
			_sut.Update(_VALID_WORKSPACE_ARTIFACT_ID, _folderWithoutParent);

			_mockGetByIdStrategy.Verify(getByIdStrategy => getByIdStrategy.Get(_VALID_WORKSPACE_ARTIFACT_ID, _FOLDER_WITHOUT_PARENT_ARTIFACT_ID, null), Times.Once);
		}
	}
}
