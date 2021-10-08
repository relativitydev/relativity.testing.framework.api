using System;
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
	[TestOf(typeof(IFolderCreateStrategy))]
	internal class FolderCreateStrategyFixture
	{
		private const int _VALID_WORKSPACE_ARTIFACT_ID = 1;
		private const int _VALID_CUSTOM_PARENT_FOLDER_ARTIFACT_ID = 2;
		private const int _WORKSPACE_ROOT_FOLDER_ARTIFACT_ID = 2;
		private const int _CREATED_FOLDER_ARTIFACT_ID = 3;
		private const string _FOLDER_NAME = "Test folder name";
		private const string _CREATE_URL = "Relativity.Services.Folder.IFolderModule/Folder%20Manager/CreateSingleAsync";

		private readonly Folder _folderToCreateWithCustomParentSet = new Folder
		{
			Name = _FOLDER_NAME,
			ParentFolder = new NamedArtifact
			{
				ArtifactID = _VALID_CUSTOM_PARENT_FOLDER_ARTIFACT_ID
			}
		};

		private readonly Folder _folderToCreateWithoutCustomParentSet = new Folder
		{
			Name = _FOLDER_NAME
		};

		private readonly NamedArtifact _expectedCustomParentFolder = new NamedArtifact
		{
			ArtifactID = _VALID_CUSTOM_PARENT_FOLDER_ARTIFACT_ID,
			Name = "Custom Parent Folder"
		};

		private readonly NamedArtifact _expectedWorkspaceRootFolder = new NamedArtifact
		{
			ArtifactID = _WORKSPACE_ROOT_FOLDER_ARTIFACT_ID,
			Name = "Workspace Root Folder"
		};

		private Mock<IRestService> _mockRestService;
		private Mock<IFolderGetByIdStrategy> _mockGetByIdStrategy;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private IFolderCreateStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockGetByIdStrategy = new Mock<IFolderGetByIdStrategy>();
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_sut = new FolderCreateStrategy(_mockRestService.Object, _mockGetByIdStrategy.Object, _mockWorkspaceIdValidator.Object);
		}

		[Test]
		public void Create_WithNullFolder_ThrowsArgumentNullException()
		{
			Folder folder = null;

			Assert.Throws<ArgumentNullException>(() => _sut.Create(_VALID_WORKSPACE_ARTIFACT_ID, folder));
		}

		[Test]
		public void Create_WithAnyParameters_CallsWorkspaceIdValidator()
		{
			SetupMockRestService();

			_sut.Create(_VALID_WORKSPACE_ARTIFACT_ID, _folderToCreateWithCustomParentSet);

			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_VALID_WORKSPACE_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Create_WithValidParameters_CallsRestServiceWithExpectedParameters()
		{
			SetupMockRestService();

			_sut.Create(_VALID_WORKSPACE_ARTIFACT_ID, _folderToCreateWithCustomParentSet);

			_mockRestService.Verify(restService => restService.Post<int>(_CREATE_URL, It.Is<FolderRequest>(request => request.WorkspaceArtifactID == _VALID_WORKSPACE_ARTIFACT_ID && request.Model.Name == _folderToCreateWithCustomParentSet.Name && request.Model.ParentFolder.ArtifactID == _folderToCreateWithCustomParentSet.ParentFolder.ArtifactID), 2, null), Times.Once);
		}

		[Test]
		public void Create_WithValidParametersAndFolderWithCustomParentFolderSet_CallsGetFolderByIdStrastegy()
		{
			SetupMockRestService();
			SetupMockGetByIdStrategy(_expectedCustomParentFolder, _VALID_CUSTOM_PARENT_FOLDER_ARTIFACT_ID);

			_sut.Create(_VALID_WORKSPACE_ARTIFACT_ID, _folderToCreateWithCustomParentSet);

			_mockGetByIdStrategy.Verify(getByIdStrategy => getByIdStrategy.Get(_VALID_WORKSPACE_ARTIFACT_ID, _CREATED_FOLDER_ARTIFACT_ID, _VALID_CUSTOM_PARENT_FOLDER_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Create_WithValidParametersAndFolderWithCustomParentFolderSet_ReturnsFolderFromGetByIdStrategy()
		{
			SetupMockRestService();
			Folder expectedFolder = SetupMockGetByIdStrategy(_expectedCustomParentFolder, _VALID_CUSTOM_PARENT_FOLDER_ARTIFACT_ID);

			Folder createdFolder = _sut.Create(_VALID_WORKSPACE_ARTIFACT_ID, _folderToCreateWithCustomParentSet);

			createdFolder.Should().BeEquivalentTo(expectedFolder);
		}

		[Test]
		public void Create_WithValidParametersAndFolderWithoutCustomParentFolderSet_CallsGetFolderByIdStrastegy()
		{
			SetupMockRestService();
			SetupMockGetByIdStrategy(_expectedWorkspaceRootFolder, null);

			_sut.Create(_VALID_WORKSPACE_ARTIFACT_ID, _folderToCreateWithoutCustomParentSet);

			_mockGetByIdStrategy.Verify(getByIdStrategy => getByIdStrategy.Get(_VALID_WORKSPACE_ARTIFACT_ID, _CREATED_FOLDER_ARTIFACT_ID, null), Times.Once);
		}

		[Test]
		public void Create_WithValidParametersAndFolderWithoutCustomParentFolderSet_ReturnsFolderFromGetByIdStrategy()
		{
			SetupMockRestService();
			Folder expectedFolder = SetupMockGetByIdStrategy(_expectedWorkspaceRootFolder, null);

			Folder createdFolder = _sut.Create(_VALID_WORKSPACE_ARTIFACT_ID, _folderToCreateWithoutCustomParentSet);

			createdFolder.Should().BeEquivalentTo(expectedFolder);
		}

		private Folder SetupMockGetByIdStrategy(NamedArtifact expectedFolderParent, int? parentFolderArtifactID)
		{
			Folder expectedCreatedFolder = GetExpectedFolder(expectedFolderParent);
			_mockGetByIdStrategy.Setup(getByIdStrategy => getByIdStrategy.Get(_VALID_WORKSPACE_ARTIFACT_ID, _CREATED_FOLDER_ARTIFACT_ID, parentFolderArtifactID)).Returns(expectedCreatedFolder);
			return expectedCreatedFolder;
		}

		private static Folder GetExpectedFolder(NamedArtifact parentFolder)
		{
			return new Folder
			{
				Name = _FOLDER_NAME,
				ParentFolder = parentFolder,
				AccessControlListIsInherited = true,
				HasChildren = false,
				Permissions = new FolderPermission
				{
					Add = true,
					Delete = true,
					Edit = true,
					Secure = true
				},
				SystemCreatedOn = DateTime.Now,
				SystemLastModifiedOn = DateTime.Now
			};
		}

		private void SetupMockRestService()
		{
			_mockRestService.Setup(restService => restService.Post<int>(_CREATE_URL, It.Is<FolderRequest>(request => request.WorkspaceArtifactID == _VALID_WORKSPACE_ARTIFACT_ID && request.Model != null && request.Model.Name == _FOLDER_NAME), 2, null)).Returns(_CREATED_FOLDER_ARTIFACT_ID);
		}
	}
}
