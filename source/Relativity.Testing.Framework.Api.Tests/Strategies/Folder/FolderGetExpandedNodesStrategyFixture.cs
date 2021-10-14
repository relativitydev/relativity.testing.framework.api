using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
	[TestOf(typeof(IFolderGetExpandedNodesStrategy))]
	internal class FolderGetExpandedNodesStrategyFixture
	{
		private const string _GET_URL = "Relativity.Services.Folder.IFolderModule/Folder%20Manager/GetFolderTreeAsync";
		private const int _VALID_WORKSPACE_ARTIFACT_ID = 1;
		private const int _VALID_FOLDER_ARTIFACT_ID = 2;
		private const int _VALID_SELECTED_FOLDER_ARTIFACT_ID = 3;

		private readonly List<int> _validFolderArtifactIDs = new List<int>
		{
			_VALID_FOLDER_ARTIFACT_ID,
			_VALID_SELECTED_FOLDER_ARTIFACT_ID
		};

		private readonly FolderDto _expandedFolder = new FolderDto
		{
			ArtifactID = _VALID_FOLDER_ARTIFACT_ID,
			Name = "Folder",
			AccessControlListIsInherited = false,
			HasChildren = false,
			Permissions = new FolderPermissionDto
			{
				Add = true,
				Delete = false,
				Edit = false,
				Secure = true
			},
			Selected = false,
			SystemCreatedOn = DateTime.Now,
			SystemLastModifiedOn = DateTime.Now
		};

		private readonly FolderDto _selectedFolder = new FolderDto
		{
			ArtifactID = _VALID_SELECTED_FOLDER_ARTIFACT_ID,
			Name = "Selected Folder",
			ParentFolder = new NamedArtifact
			{
				Name = "Folder",
				ArtifactID = _VALID_FOLDER_ARTIFACT_ID
			},
			AccessControlListIsInherited = true,
			HasChildren = false,
			Permissions = new FolderPermissionDto
			{
				Add = true,
				Delete = true,
				Edit = true,
				Secure = false
			},
			Selected = true,
			SystemCreatedOn = DateTime.Now,
			SystemLastModifiedOn = DateTime.Now
		};

		private Mock<IRestService> _mockRestService;
		private Mock<IWorkspaceIdValidator> _mockWorkspaceIdValidator;
		private IFolderGetExpandedNodesStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockRestService.Setup(restService => restService.Post<List<FolderDto>>(_GET_URL, It.IsAny<object>(), 2, null)).Returns(new List<FolderDto> { _expandedFolder, _selectedFolder });
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();

			_sut = new FolderGetExpandedNodesStrategy(
				_mockRestService.Object,
				_mockWorkspaceIdValidator.Object);
		}

		[Test]
		public void Get_WithAnyParameters_CallsWorkspaceIDValidator()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _validFolderArtifactIDs, _VALID_FOLDER_ARTIFACT_ID);

			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_VALID_WORKSPACE_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Get_WithValidParameters_CallsRestService()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _validFolderArtifactIDs, _VALID_FOLDER_ARTIFACT_ID);

			_mockRestService.Verify(restService => restService.Post<List<FolderDto>>(_GET_URL, It.IsAny<object>(), 2, null), Times.Once);
		}

		[Test]
		public void Get_WithValidParameters_ReturnsExpectedFolders()
		{
			List<Folder> expectedFolders = GetExpectedFolders();

			List<Folder> folders = _sut.Get(_VALID_WORKSPACE_ARTIFACT_ID, _validFolderArtifactIDs, _VALID_FOLDER_ARTIFACT_ID);

			folders.Should().NotBeNullOrEmpty();
			folders.Should().BeEquivalentTo(expectedFolders);
		}

		private List<Folder> GetExpectedFolders()
		{
			return new List<Folder>
			{
				GetExpectedFolder(_expandedFolder),
				GetExpectedFolder(_selectedFolder),
			};
		}

		private static Folder GetExpectedFolder(FolderDto dto)
		{
			return new Folder
			{
				ArtifactID = dto.ArtifactID,
				Name = dto.Name,
				ParentFolder = dto.ParentFolder,
				AccessControlListIsInherited = dto.AccessControlListIsInherited,
				HasChildren = dto.HasChildren,
				Permissions = new FolderPermission
				{
					Add = dto.Permissions.Add,
					Delete = dto.Permissions.Delete,
					Edit = dto.Permissions.Edit,
					Secure = dto.Permissions.Secure
				},
				Selected = dto.Selected,
				SystemCreatedOn = dto.SystemCreatedOn,
				SystemLastModifiedOn = dto.SystemLastModifiedOn
			};
		}
	}
}
