using System;
using System.Collections.Generic;
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
	[TestOf(typeof(IFolderGetSubfoldersStrategy))]
	internal class FolderGetSubfoldersStrategyFixture
	{
		private const string _GET_URL = "Relativity.Services.Folder.IFolderModule/Folder%20Manager/GetChildrenAsync";
		private const int _VALID_WORKSPACE_ARTIFACT_ID = 1;
		private const int _VALID_PARENT_FOLDER_ARTIFACT_ID = 2;

		private readonly FolderDto _subfolderDto = new FolderDto
		{
			ArtifactID = 3,
			Name = "Subfolder",
			ParentFolder = new NamedArtifact
			{
				Name = "Parent Folder",
				ArtifactID = _VALID_PARENT_FOLDER_ARTIFACT_ID
			},
			AccessControlListIsInherited = false,
			HasChildren = false,
			Permissions = new FolderPermissionDto
			{
				Add = true,
				Delete = false,
				Edit = false,
				Secure = true
			},
			SystemCreatedOn = DateTime.Now,
			SystemLastModifiedOn = DateTime.Now
		};

		private Mock<IRestService> _mockRestService;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private Mock<IArtifactIdValidator> _mockArtifactIdValidator;
		private IFolderGetSubfoldersStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockRestService.Setup(restService => restService.Post<List<FolderDto>>(_GET_URL, It.IsAny<object>(), 2, null)).Returns(new List<FolderDto> { _subfolderDto });
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();
			_mockArtifactIdValidator = new Mock<IArtifactIdValidator>();

			_sut = new FolderGetSubfoldersStrategy(
				_mockRestService.Object,
				_mockWorkspaceIdValidator.Object,
				_mockArtifactIdValidator.Object);
		}

		[Test]
		public void Get_WithAnyParameters_CallsWorkspaceIdValidator()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_PARENT_FOLDER_ARTIFACT_ID);

			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_VALID_WORKSPACE_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Get_WithAnyParameters_CallsArtifactIdValidatorForParentFolderArtifactId()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_PARENT_FOLDER_ARTIFACT_ID);

			_mockArtifactIdValidator.Verify(validator => validator.Validate(_VALID_PARENT_FOLDER_ARTIFACT_ID, "Parent Folder"), Times.Once);
		}

		[Test]
		public void Get_WithAnyParameters_CallsRestService()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_PARENT_FOLDER_ARTIFACT_ID);

			_mockRestService.Verify(restService => restService.Post<List<FolderDto>>(_GET_URL, It.IsAny<object>(), 2, null), Times.Once);
		}

		[Test]
		public void Get_WithValidParameters_ReturnsExpectedFolders()
		{
			List<Folder> expectedFolders = GetExpectedFolders();

			List<Folder> folders = _sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _VALID_PARENT_FOLDER_ARTIFACT_ID);

			folders.Should().NotBeNullOrEmpty();
			folders.Should().BeEquivalentTo(expectedFolders);
		}

		private List<Folder> GetExpectedFolders()
		{
			return new List<Folder>
			{
				new Folder
				{
					ArtifactID = _subfolderDto.ArtifactID,
					Name = _subfolderDto.Name,
					ParentFolder = _subfolderDto.ParentFolder,
					AccessControlListIsInherited = _subfolderDto.AccessControlListIsInherited,
					HasChildren = _subfolderDto.HasChildren,
					Permissions = new FolderPermission
					{
						Add = _subfolderDto.Permissions.Add,
						Delete = _subfolderDto.Permissions.Delete,
						Edit = _subfolderDto.Permissions.Edit,
						Secure = _subfolderDto.Permissions.Secure
					},
					SystemCreatedOn = _subfolderDto.SystemCreatedOn,
					SystemLastModifiedOn = _subfolderDto.SystemLastModifiedOn
				}
			};
		}
	}
}
