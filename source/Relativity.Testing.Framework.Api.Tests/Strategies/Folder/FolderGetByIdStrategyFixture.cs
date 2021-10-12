using System;
using System.Collections.Generic;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Api.Validators;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(IFolderGetByIdStrategy))]
	internal class FolderGetByIdStrategyFixture
	{
		private const int _VALID_WORKSPACE_ARTIFACT_ID = 1;
		private const int _VALID_FOLDER_ARTIFACT_ID = 2;
		private const int _VALID_FOLDER_WITH_PARENT_FOLDER_ARTIFACT_ID = 3;
		private const int _VALID_PARENT_FOLDER_ARTIFACT_ID = 4;
		private const int _WORKSPACE_ROOT_FOLDER_ARTIFACT_ID = 5;
		private const string _WORKSPACE_ROOT_FOLDER_NAME = "Workspace Root Folder";

		private readonly Folder _workspaceRootFolder = new Folder
		{
			Name = _WORKSPACE_ROOT_FOLDER_NAME,
			ArtifactID = _WORKSPACE_ROOT_FOLDER_ARTIFACT_ID
		};

		private readonly Folder _folderWithCustomParentFolder = new Folder
		{
			ArtifactID = _VALID_FOLDER_ARTIFACT_ID,
			Name = "Folder",
			ParentFolder = new NamedArtifact
			{
				Name = "Parent Folder",
				ArtifactID = _VALID_PARENT_FOLDER_ARTIFACT_ID
			},
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

		private readonly Folder _folderWithWorkspaceRootFolderParent = new Folder
		{
			ArtifactID = _VALID_FOLDER_WITH_PARENT_FOLDER_ARTIFACT_ID,
			Name = "Folder with root parent",
			ParentFolder = new NamedArtifact
			{
				Name = _WORKSPACE_ROOT_FOLDER_NAME,
				ArtifactID = _WORKSPACE_ROOT_FOLDER_ARTIFACT_ID
			},
			AccessControlListIsInherited = false,
			HasChildren = false,
			Permissions = new FolderPermission
			{
				Add = true,
				Delete = false,
				Edit = false,
				Secure = true
			},
			SystemCreatedOn = DateTime.Now,
			SystemLastModifiedOn = DateTime.Now
		};

		private Mock<IFolderGetWorkspaceRootFolderStrategy> _mockGetWorkspaceRootFolderStrategy;
		private Mock<IFolderGetSubfoldersStrategy> _mockGetSubfoldersStrategy;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private Mock<IArtifactIdValidator> _mockArtifactIdValidator;
		private IFolderGetByIdStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			SetupMockGetWorkspaceRootFolderStrategy();
			SetupMockGetSubfoldersStrategy();
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_mockArtifactIdValidator = new Mock<IArtifactIdValidator>();

			_sut = new FolderGetByIdStrategy(
				_mockGetSubfoldersStrategy.Object,
				_mockGetWorkspaceRootFolderStrategy.Object,
				_mockWorkspaceIdValidator.Object,
				_mockArtifactIdValidator.Object);
		}

		[Test]
		public void Get_WithAnyParameters_CallsWorkspaceIdValidator()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID, _VALID_PARENT_FOLDER_ARTIFACT_ID);

			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_VALID_WORKSPACE_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Get_WithAnyParameters_CallsArtifactIdValidatorForFolderArtifactId()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID, _VALID_PARENT_FOLDER_ARTIFACT_ID);

			_mockArtifactIdValidator.Verify(validator => validator.Validate(_VALID_FOLDER_ARTIFACT_ID, "Folder"), Times.Once);
		}

		[Test]
		public void Get_WithAnyParameters_CallsArtifactIdValidatorForParentFolderArtifactId()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID, _VALID_PARENT_FOLDER_ARTIFACT_ID);

			_mockArtifactIdValidator.Verify(validator => validator.Validate(_VALID_PARENT_FOLDER_ARTIFACT_ID, "Parent Folder"), Times.Once);
		}

		[Test]
		public void Get_WithoutParentFolderArtifactID_CallsGetWorkspaceRootFolderStrategy()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_WITH_PARENT_FOLDER_ARTIFACT_ID, null);

			_mockGetWorkspaceRootFolderStrategy.Verify(strategy => strategy.Get(_VALID_WORKSPACE_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Get_WithParentFolderArtifactID_DoesNotCallsGetWorkspaceRootFolderStrategy()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID, _VALID_PARENT_FOLDER_ARTIFACT_ID);

			_mockGetWorkspaceRootFolderStrategy.Verify(strategy => strategy.Get(_VALID_WORKSPACE_ARTIFACT_ID), Times.Never);
		}

		[Test]
		public void Get_WithoutParentFolderArtifactID_CallsGetSubfoldersStrategyWithWorkspaceRootFolderArtifactID()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_WITH_PARENT_FOLDER_ARTIFACT_ID, null);

			_mockGetSubfoldersStrategy.Verify(strategy => strategy.Get(_VALID_WORKSPACE_ARTIFACT_ID, _WORKSPACE_ROOT_FOLDER_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Get_WithParentFolderArtifactID_CallsGetSubfoldersStrategyWithParentFolderArtifactID()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_ARTIFACT_ID, _VALID_PARENT_FOLDER_ARTIFACT_ID);

			_mockGetSubfoldersStrategy.Verify(strategy => strategy.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_PARENT_FOLDER_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Get_WithValidParameters_ReturnsExpectedFolder()
		{
			Folder folder = _sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_FOLDER_WITH_PARENT_FOLDER_ARTIFACT_ID, null);

			folder.Should().NotBeNull();
			folder.Should().BeEquivalentTo(_folderWithWorkspaceRootFolderParent);
		}

		private void SetupMockGetSubfoldersStrategy()
		{
			_mockGetSubfoldersStrategy = new Mock<IFolderGetSubfoldersStrategy>();

			var expectedResultForGetWithParentFolderArtifactID = new List<Folder>
			{
				_folderWithCustomParentFolder
			};
			var expectedResultForGetWithoutParentFolderArtifactID = new List<Folder>
			{
				_folderWithWorkspaceRootFolderParent
			};
			_mockGetSubfoldersStrategy.Setup(strategy => strategy.Get(_VALID_WORKSPACE_ARTIFACT_ID, _WORKSPACE_ROOT_FOLDER_ARTIFACT_ID))
				.Returns(expectedResultForGetWithoutParentFolderArtifactID);
			_mockGetSubfoldersStrategy.Setup(strategy => strategy.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_PARENT_FOLDER_ARTIFACT_ID))
				.Returns(expectedResultForGetWithParentFolderArtifactID);
		}

		private void SetupMockGetWorkspaceRootFolderStrategy()
		{
			_mockGetWorkspaceRootFolderStrategy = new Mock<IFolderGetWorkspaceRootFolderStrategy>();
			_mockGetWorkspaceRootFolderStrategy.Setup(strategy => strategy.Get(_VALID_WORKSPACE_ARTIFACT_ID))
				.Returns(_workspaceRootFolder);
		}
	}
}
