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
	[TestOf(typeof(IFolderGetWorkspaceRootFolderStrategy))]
	internal class FolderGetWorkspaceRootFolderStrategyFixture
	{
		private const int _VALID_WORKSPACE_ARTIFACT_ID = 1;
		private const string _GET_URL = "Relativity.Services.Folder.IFolderModule/Folder%20Manager/GetWorkspaceRootAsync";

		private readonly FolderDto _folderDto = new FolderDto
		{
			ArtifactID = 1,
			Name = "Workspace root folder",
			ParentFolder = new NamedArtifact(),
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
		private IFolderGetWorkspaceRootFolderStrategy _sut;

		[SetUp]
		public void SetUp()
		{
			_mockRestService = new Mock<IRestService>();
			_mockRestService.Setup(restService => restService.Post<FolderDto>(_GET_URL, It.IsAny<object>(), 2, null))
				.Returns(_folderDto);
			_mockWorkspaceIdValidator = new Mock<IWorkspaceIdValidator>();

			_sut = new FolderGetWorkspaceRootFolderStrategy(
				_mockRestService.Object,
				_mockWorkspaceIdValidator.Object);
		}

		[Test]
		public void Get_WithAnyParameters_CallsWorkspaceIdValidator()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID);

			_mockWorkspaceIdValidator.Verify(validator => validator.Validate(_VALID_WORKSPACE_ARTIFACT_ID), Times.Once);
		}

		[Test]
		public void Get_WithValidParameters_CallsRestService()
		{
			_sut.Get(_VALID_WORKSPACE_ARTIFACT_ID);

			_mockRestService.Verify(restService => restService.Post<FolderDto>(_GET_URL, It.IsAny<object>(), 2, null), Times.Once);
		}

		[Test]
		public void Get_WithValidParameters_ReturnsExpectedFolder()
		{
			var expectedFolder = new Folder
			{
				ArtifactID = _folderDto.ArtifactID,
				Name = _folderDto.Name,
				ParentFolder = _folderDto.ParentFolder,
				AccessControlListIsInherited = _folderDto.AccessControlListIsInherited,
				HasChildren = _folderDto.HasChildren,
				Permissions = new FolderPermission
				{
					Add = _folderDto.Permissions.Add,
					Delete = _folderDto.Permissions.Delete,
					Edit = _folderDto.Permissions.Edit,
					Secure = _folderDto.Permissions.Secure
				},
				SystemCreatedOn = _folderDto.SystemCreatedOn,
				SystemLastModifiedOn = _folderDto.SystemLastModifiedOn
			};

			Folder folder = _sut.Get(_VALID_WORKSPACE_ARTIFACT_ID);

			folder.Should().NotBeNull();
			folder.Should().BeEquivalentTo(expectedFolder);
		}
	}
}
