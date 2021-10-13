using System;
using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using Relativity.Testing.Framework.Api.Strategies;
using Relativity.Testing.Framework.Models;

namespace Relativity.Testing.Framework.Api.Tests.Strategies
{
	[TestFixture]
	[TestOf(typeof(FolderDtoMappingExtensions))]
	internal class FolderDtoMappingExtensionsFixture
	{
		[Test]
		public void DoMappingToFolder_MapsAllFolderProperties()
		{
			int folderArtifactID = 2;
			string folderName = "Folder Name";
			string subfolderName = "Subfolder Name";
			int subfolderArtifactID = 3;

			var parentFolder = new NamedArtifact
			{
				Name = "Parent Folder",
				ArtifactID = 1
			};

			var dto = new FolderDto
			{
				ArtifactID = folderArtifactID,
				Name = folderName,
				ParentFolder = parentFolder,
				AccessControlListIsInherited = true,
				HasChildren = true,
				Selected = false,
				Permissions = new FolderPermissionDto
				{
					Add = true,
					Delete = true,
					Edit = false,
					Secure = false
				},
				Children = new List<FolderDto>
				{
					new FolderDto
					{
						Name = "Subfolder Name",
						ArtifactID = subfolderArtifactID,
						ParentFolder = new NamedArtifact
						{
							Name = folderName,
							ArtifactID = folderArtifactID
						}
					}
				},
				SystemCreatedOn = DateTime.Now,
				SystemLastModifiedOn = DateTime.Now
			};

			var expectedFolder = new Folder
			{
				ArtifactID = dto.ArtifactID,
				Name = dto.Name,
				ParentFolder = dto.ParentFolder,
				AccessControlListIsInherited = dto.AccessControlListIsInherited,
				HasChildren = dto.HasChildren,
				Selected = dto.Selected,
				Permissions = new FolderPermission
				{
					Add = dto.Permissions.Add,
					Delete = dto.Permissions.Delete,
					Edit = dto.Permissions.Edit,
					Secure = dto.Permissions.Secure,
				},
				Children = new List<Folder>
				{
					new Folder
					{
						Name = subfolderName,
						ArtifactID = subfolderArtifactID,
						ParentFolder = new NamedArtifact
						{
							Name = folderName,
							ArtifactID = folderArtifactID
						}
					}
				},
				SystemCreatedOn = dto.SystemCreatedOn,
				SystemLastModifiedOn = dto.SystemLastModifiedOn
			};

			Folder result = dto.DoMappingToFolder();

			result.Should().BeEquivalentTo(expectedFolder);
		}

		[Test]
		public void DoMappingToFolderPermission_MapsAllFolderPermissionProperties()
		{
			var dto = new FolderPermissionDto
			{
				Add = true,
				Delete = false,
				Edit = true,
				Secure = false
			};

			var expectedFolderPermission = new FolderPermission
			{
				Add = dto.Add,
				Delete = dto.Delete,
				Edit = dto.Edit,
				Secure = dto.Secure,
			};

			FolderPermission result = dto.DoMappingToFolderPermission();

			result.Should().BeEquivalentTo(expectedFolderPermission);
		}

		[Test]
		public void DoMappingToFolderMoveResponse_MapsAllFolderMoveResponseProperties()
		{
			var dto = new FolderMoveResponseDto
			{
				ProcessState = "Test Process State",
				TotalOperations = 666,
				OperationsCompleted = 660
			};

			var expectedFolderStatus = new FolderMoveResponse
			{
				ProcessState = dto.ProcessState,
				TotalOperations = dto.TotalOperations,
				OperationsCompleted = dto.OperationsCompleted
			};

			FolderMoveResponse result = dto.DoMappingToFolderMoveResponse();

			result.Should().BeEquivalentTo(expectedFolderStatus);
		}
	}
}
